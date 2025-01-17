;******************************************************************************* 
;*          Thresholding Library                                               * 
;*                                                                             * 
;*                                              * 
;******************************************************************************* 
.code 

ConvertToGrayScale proc
	push rbp
	mov rbp, rsp
	push rsi
	push rdi
	push rcx
	push rdx
	push rbx
	push r8
	push r9
	push r10
	push r11
	push r12
	push r13
	push r14
	push r15


	; przypisanie parametrów do sta³ych
	mov RSI, RCX	; SourcePointer -> RSI
	mov RDI, RDX	; ResultPointer -> RDI
	mov EBX, R8D	; Width -> ECX
	mov EDX, R9D	; Height -> EDX
	mov R15D,  dword ptr [RBP + 48]	; stride -> R15D
	mov R10D,  dword ptr [RBP + 56]	; start -> R10D
	mov R11D, dword ptr  [RBP + 64]	; endRow -> R11D

	mov R12D, R10D	; StartRow->R12D (y)
outer_loop:
	cmp R12D, R11D	; y=EndRow?
	jge done

	xor R13D, R13D
inner_loop:
	cmp R13D, EBX
	jge end_inner_loop

	; obliczanie przesuniecia dla kazdego piksela
	mov ECX, R12D
	imul ECX, R15D
	mov EBP, R13D
	imul EBP, 4
	add ECX, EBP
	
	;RCX = indeks aktualnego piksela

	; pobranie wartosci kazdego piksela [movzx - wypelnia zerami jesli ]
	movzx r8d, BYTE PTR [RSI + RCX]			; blue
	movzx r9d, BYTE PTR [RSI + RCX + 1]		; green
	movzx r14d, BYTE PTR [RSI + RCX + 2]		; red\

	imul r8d, 76
	imul r9d, 150
	imul r14d, 29

	add r8d, r9d
	add r8d, r14d
	shr r8d, 8

	mov BYTE PTR [RDI + RCX], r8b ; blue
	mov BYTE PTR [RDI + RCX + 1], r8b ; green
	mov BYTE PTR [RDI + RCX + 2], r8b ; red

	inc R13D
	jmp inner_loop

end_inner_loop:
	inc R12D
	jmp outer_loop

done:
	mov rax, rsi
	pop rsi
	pop rdi
	pop rcx
	pop rdx
	pop rbx
	pop r8
	pop r9
	pop r10
	pop r11
	pop r12
	pop r13
	pop r14
	pop r15
	pop rbp
	ret

ConvertToGrayScale endp
end