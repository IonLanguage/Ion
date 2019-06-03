; ModuleID = 'entry'
source_filename = "entry"

%Person.0 = type <{ i32, i8* }>

@str_0 = private unnamed_addr constant [4 x i8] c"Joe\00"

define void @test() {
entry:
  %joe = alloca %Person.0
  store %Person.0 <{ i32 25, i8* getelementptr inbounds ([4 x i8], [4 x i8]* @str_0, i32 0, i32 0) }>, %Person.0* %joe
  %futureAge = alloca i32
  %joe.age = getelementptr inbounds %Person.0, %Person.0* %joe, i32 0, i32 0
  %tmp = add i32* %joe.age, i32 5
  store i32* %tmp, i32* %futureAge
  ret void
}
