; ModuleID = 'entry'
source_filename = "entry"

define void @Test(i8*) {
entry:
  ret void
}

define void @Main() {
entry:
  %anonymous_157 = call void @Test()
  ret void
}
