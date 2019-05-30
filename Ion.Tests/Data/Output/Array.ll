; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %array = alloca [3 x i32]
  store [3 x i32] [i32 1, i32 2, i32 3], [3 x i32]* %array
  ret void
}
