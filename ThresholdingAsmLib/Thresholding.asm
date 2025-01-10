;******************************************************************************* 
;*          Thresholding Library                                               * 
;*                                                                             * 
;*                                              * 
;******************************************************************************* 
.code 

ConvertToGrayScale proc
	; przypisanie parametrów do sta³ych
	mov RSI, RCX	; SourcePointer -> RSI
	mov RDI, RDX	; ResultPointer -> RDI
	mov ECX, R8D	; Width -> R8D
	mov EDX, R9D	; Height -> R9D
	mov r14d,  dword ptr [RBP + 56]	; Stride -> r14d
	mov EBX,  dword ptr [RBP + 48]	; StartRow -> EBX
	mov R10D, dword ptr  [RBP + 40]	; EndRow -> R10D
	mov R11D, 4		; bytesForPixel -> R11D

	mov R12D, r14d	; StartRow->R12D (y)
outer_loop:
	cmp R12D, R10D	; y=EndRow?
	jge done

	xor R13D, R13D
inner_loop:
	cmp R13D, R8D
	jge end_inner_loop

	
	mov ECX, R12D
	imul ECX, ebx
	mov ESI, R13D
	imul ESI, R11D
	add ECX, ESI

	mov BYTE PTR [RDI + RCX + 1], 255 ; 
	mov BYTE PTR [RDI + RCX + 2], 0 ; blue
	mov BYTE PTR [RDI + RCX + 3], 0 ; blue
	mov BYTE PTR [RDI + RCX + 4], 255 ; blue

	inc R13D
	jmp inner_loop
	ret

end_inner_loop:
	inc R12D
	jmp outer_loop

done:
	mov rax, R14
	ret

ConvertToGrayScale endp
end