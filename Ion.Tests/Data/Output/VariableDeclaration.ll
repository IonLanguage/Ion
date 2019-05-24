; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %variable = alloca i32
  store i32 5, i32* %variable
  ret void
}
