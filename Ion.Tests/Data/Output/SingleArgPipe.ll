; ModuleID = 'entry'
source_filename = "entry"

define void @Test(i8*) {
entry:
  ret void
}

define void @main() {
entry:
  %anonymous_171 = call void @Test()
  ret void
}
