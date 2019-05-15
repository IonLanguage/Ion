; ModuleID = 'entry'
source_filename = "entry"

define i32 @Main() {
entry:
  %returnValue = alloca i32
  store i32 0, i32* %returnValue
  ret i32* %returnValue
}
