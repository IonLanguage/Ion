; ModuleID = 'entry'
source_filename = "entry"

%TestStruct = type <{ i32 }>

define void @Test() {
entry:
  %instance = alloca %TestStruct
  store %TestStruct <{ i32 5 }>, %TestStruct* %instance
  ret void
}
