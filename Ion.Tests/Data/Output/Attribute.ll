; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  ret void
}

define void @main() {
entry:
  %.anonymous.5 = call void @test()
  ret void
}
