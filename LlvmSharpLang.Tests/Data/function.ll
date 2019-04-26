; ModuleID = 'entry'
source_filename = "entry"
target datalayout = "e-m:e-i64:64-f80:128-n8:16:32:64-S128"

define i32 @sum(i32, i32) #0 {
entry:
  %tmp = add i32 %0, %1
  ret i32 %tmp
}

define i32 @helloWorld(i32, float) {
entry:
  %anonymous = call i32 @helloWorld()
  ret void
}

attributes #0 = { "no-frame-pointer-elim"="true" }
