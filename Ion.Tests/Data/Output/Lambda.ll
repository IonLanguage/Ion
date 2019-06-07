; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %.anonymous.7 = call void @.lambda.0()
  ret void
}

define void @.lambda.0() {
.block.1:
  %pi = alloca float
  store double 3.140000e+00, float* %pi
  ret void
}
