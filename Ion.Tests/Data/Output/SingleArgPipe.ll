; ModuleID = 'entry'
source_filename = "entry"

define void @Test(i8*) {
entry:
  ret void
}

define void @main() {
entry:
  %anonymous_184 = call void @Test()
  ret void
}
