; ModuleID = 'entry'
source_filename = "entry"

define void @Test1(i8*) {
entry:
  ret void
}

define void @main() {
entry:
  %anonymous_170 = call void @Test1()
  ret void
}
