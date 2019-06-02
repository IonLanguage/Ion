; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %anonymous_7 = call void @lambda_0()
  ret void
}

define void @lambda_0() {
anonymous_3:
  %pi = alloca float
  store double 3.140000e+00, float* %pi
  ret void
}
