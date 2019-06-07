; ModuleID = 'entry'
source_filename = "entry"

define void @test() {
entry:
  ret void
}

define void @main() {
entry:
  %.anonymous.4 = call void @test()
  ret void
}
