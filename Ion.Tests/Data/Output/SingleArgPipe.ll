; ModuleID = 'entry'
source_filename = "entry"

@str_2 = private unnamed_addr constant [12 x i8] c"Test string\00"

define void @Test(i8*) {
entry:
  ret void
}

define void @main() {
entry:
  %anonymous_184 = call void @Test(i8* getelementptr inbounds ([12 x i8], [12 x i8]* @str_2, i32 0, i32 0))
  ret void
}
