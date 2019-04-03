# zz_vs2019_fsc_stack_overflow_repro

Small repro for stack overflow in fsc.exe - Visual Studio 2019.

Observed behavior is that it takes about 15 seconds to build, and fails.

Observed output in Visual Studio 2019 version 16.0.0 is:

```
1>------ Build started: Project: vs2019_so, Configuration: Release Any CPU ------
1>c:\program files (x86)\microsoft visual studio\2019\enterprise\common7\ide\commonextensions\microsoft\fsharp\Microsoft.FSharp.targets(277,9): error MSB6006: "fsc.exe" exited with code -1073741571.
1>Done building project "vs2019_so.fsproj" -- FAILED.
1>
1>Build FAILED.
========== Build: 0 succeeded, 1 failed, 0 up-to-date, 0 skipped ==========
```

The error code `-1073741571` is `0xC00000FD`, which I suspsect to be stack overflow. Here's what `windbg` has to say:

```
0:002> !error 0xc00000fd
Error code: (NTSTATUS) 0xc00000fd (3221225725) - A new guard page for the stack cannot be created.
```

(I have not debugged the fsc.exe crash.)
