; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  ret void
}

define void @Main() {
entry:
  %anonymous_8 = call void @test()
  ret void
}
