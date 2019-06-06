; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  %stringType = alloca i32 addrspace(32)*
  ret void
}
