; ModuleID = 'entry'
source_filename = "entry"

define void @main() {
entry:
  %three = alloca i32
  store i32 3, i32* %three
  ret void
}
