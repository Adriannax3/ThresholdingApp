;******************************************************************************* 
;*          Laboratory 1 simple assembly procedure call                        * 
;*                                                                             * 
;*          Standard Windows memory model                                      * 
;******************************************************************************* 
.code 
;******************************************************************************* 
;*                                                                             * 
;*       Assembler procedure MyProc1 changes Flags register in X64 mode        * 
;*                                                                             * 
;*          Input:  x: QWORD (C++ int type), y: QWORD (C++ int type)           * 
;*          x in RCX, y in RDX                                                 
;* 
;*          Output: z: QWORD (C++ int type) in the RAX register                * 
;*                                                                             * 
;******************************************************************************* 
MyProc1 proc
add RCX, RDX
mov RAX, RCX
ret
MyProc1 endp
end