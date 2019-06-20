using System;
using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class Function : Construct
    {
        public Attribute[] Attributes { get; set; }

        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public Function()
        {
            this.Attributes = new Attribute[] { };
        }

        /// <summary>
        /// Creates, assigns and returns a body block,
        /// replacing existing body.
        /// </summary>
        public Block CreateBody()
        {
            // Create a block as the body.
            Block body = new Block();

            // Set the body's name to entry.
            body.SetNameEntry();

            // Assign the created block as the body.
            this.Body = body;

            // Return the newly created body.
            return body;
        }

        public FormalArgs CreateArgs()
        {
            // Create the prototype if applicable.
            if (this.Prototype == null)
            {
                // Create the prototype.
                this.CreatePrototype();

                // Return formal arguments created by the previous invocation.
                return this.Prototype.Args;
            }

            // Create the args entity.
            this.Prototype.Args = new FormalArgs();

            // Return the newly created args.
            return this.Prototype.Args;
        }

        /// <summary>
        /// Creates a prototype for this function, overriding
        /// any existing prototype property value. Creates arguments.
        /// </summary>
        public Prototype CreatePrototype()
        {
            // Default the return type to void.
            ITypeEmitter returnType = PrimitiveTypeFactory.Void();

            // Create a new prototype instance.
            this.Prototype = new Prototype(Ion.Core.GlobalNameRegister.GetAnonymous(), null, returnType);

            // Create formal arguments after assigning prototype to avoid infinite loop.
            FormalArgs args = this.CreateArgs();

            // Assign the formal arguments.
            this.Prototype.Args = args;

            // Return the prototype.
            return this.Prototype;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
