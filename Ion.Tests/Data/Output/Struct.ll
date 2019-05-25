; ModuleID = 'entry'
source_filename = "entry"

%TestStruct = type <{ i32 }>

define void @Test() {
entry:
  %instance = alloca %TestStruct
  %anonymous_8 = alloca %TestStruct
  store %TestStruct* %anonymous_8, %TestStruct* %instance
  ret void
}
