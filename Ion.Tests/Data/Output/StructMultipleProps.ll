; ModuleID = 'entry'
source_filename = "entry"

%Person = type <{ i32, i8* }>

@str_0 = private unnamed_addr constant [4 x i8] c"Joe\00"

define void @test() {
entry:
  %joe = alloca %Person
  store %Person <{ i32 0, i8* getelementptr inbounds ([4 x i8], [4 x i8]* @str_0, i32 0, i32 0) }>, %Person* %joe
  ret void
}
