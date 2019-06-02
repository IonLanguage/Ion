; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %anonymous_19 = call void @lambda_1()
  ret void
}

define void @lambda_1() {
anonymous_14:
  %pi = alloca float
  store double 3.140000e+00, float* %pi
  ret void
}
