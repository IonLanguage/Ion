using System;
using System.Collections.Generic;
using Ion.Abstraction;

namespace Ion.Core
{
    /// <summary>
    /// Manages and oversees modules and its references. Performs
    /// simple optimization tasks such as omitting unreferenced
    /// modules from being emitted.
    /// </summary>
    public class Project
    {
        public List<Module> Modules { get; }

        public Project()
        {
            this.Modules = new List<Module>();
        }

        // TODO: Split functionality locally.
        public Dictionary<string, string> Emit()
        {
            // Ensure at least one module exists.
            if (this.Modules.Count == 0)
            {
                throw new InvalidOperationException("No modules exist");
            }

            // Create the modules map.
            Dictionary<string, Module> modules = new Dictionary<string, Module>();

            // Map modules by their identifiers.
            foreach (Module module in this.Modules)
            {
                // Extract identifier from the module's source. Ignore out size parameter.
                string identifier = LLVMSharp.LLVM.GetModuleIdentifier(module.Target, out _);

                // Register module by its identifier.
                modules.Add(identifier, module);
            }

            // Create the main module mark.
            Module mainModule = null;

            // Create a new reference map instance.
            ReferenceMap references = new ReferenceMap();

            // Process all modules.
            foreach ((string identifier, Module module) in modules)
            {
                // Loop through all the imports.
                foreach (string import in module.Imports)
                {
                    // Ensure import exists.
                    if (!modules.ContainsKey(import))
                    {
                        throw new Exception($"Import path '{import}' does not exist");
                    }

                    // Register the reference on the reference map.
                    bool successful = references.Add(identifier, import);

                    // Ensure reference was added successfully.
                    if (!successful)
                    {
                        throw new Exception($"Duplicate reference to '{import}' within module '{identifier}'");
                    }
                }
            }

            // Compute unreferenced modules.
            string[] unreferencedModules = references.ComputeUnreferenced();

            // Cleanup unreferenced modules.
            foreach (string unreferencedModule in unreferencedModules)
            {
                modules.Remove(unreferencedModule);
            }

            // Ensure a main module was set.
            if (mainModule == null)
            {
                throw new Exception("No entry function was defined");
            }

            // Create the resulting dictionary.
            Dictionary<string, string> result = new Dictionary<string, string>();

            // Emit all the modules onto the result.
            foreach ((string identifier, Module module) in modules)
            {
                // Retrieve the emitted, string form.
                string output = module.Emit();

                // Append it to the result.
                result.Add(identifier, output);
            }

            // Return the output dictionary.
            return result;
        }
    }
}
