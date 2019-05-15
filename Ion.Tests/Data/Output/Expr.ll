; ModuleID = 'entry'
source_filename = "entry"

define void @Main() {
entry:
  %explicitOne = alloca i32
  store i32 1, i32* %explicitOne
  %explicitPi = alloca float
  store double 3.140000e+00, float* %explicitPi
  %three = alloca i32
  store i32 3, i32* %three
  %pi = alloca double
  store double add (double 3.000000e+00, double 1.400000e-01), double* %pi
  %zero = alloca i32
  store i32 0, i32* %zero
  %fraction = alloca float
  store i32 0, float* %fraction
  %product = alloca i32
  store i32 30, i32* %product
  %quotient = alloca i32
  store i32 5, i32* %quotient
  %mod = alloca i32
  store i32 1, i32* %mod
  %one = alloca i32
  store i32 1, i32* %one
  ret void
}
