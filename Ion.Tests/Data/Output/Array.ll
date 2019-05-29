; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %array = alloca i32
  %anonymous_7 = alloca i32, [3 x i32] [i32 1, i32 2, i32 3]
  store [3 x i32] [i32 1, i32 2, i32 3], i32* %array
  ret void
}
