; ModuleID = 'entry'
source_filename = "entry"

define void @main() {
entry:
  %three = alloca i32
  store i32 3, i32* %three
  %pi = alloca double
  store double add (double 3.000000e+00, double 1.400000e-01), double* %pi
  ret void
}
