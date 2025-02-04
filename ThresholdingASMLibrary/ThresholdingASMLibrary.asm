;******************************************************************************* 
;*          Thresholding Library                                               * 
;*                                                                             * 
;*                                              * 
;******************************************************************************* 
.data
RED   dd 0.299
GREEN dd 0.587
BLUE  dd 0.114

.code 

ConvertToGrayScale proc
	mov rax, rdx
	push rbp
	mov rbp, rsp
	push rsi
	push rdi
	push rbx
	push r12
	push r13
	push r14
	push r15

	; Assigning parameters to constants
	mov RSI, RCX						; SourcePointer -> RSI
	mov RDI, RDX						; ResultPointer -> RDI
	mov EBX, R8D						; Width -> EBX
	mov RDX, R9							; HistogramPointer -> RDX
	mov R15D,  dword ptr [RBP + 48]		; stride -> R15D
	mov R10D,  dword ptr [RBP + 56]		; start -> R10D
	mov R11D, dword ptr  [RBP + 64]		; endRow -> R11D


	mov R12D, R10D						; StartRow->R12D  r12d=y
outer_loop:
	cmp R12D, R11D						; y==EndRow ?
	jge done

	xor R13D, R13D						; r13d=0 | r13d=x
inner_loop:
	cmp R13D, EBX						; r13d==width?
	jge end_inner_loop					; >= go to end_inner_loop

	; Calculate the offset for each pixel
	mov ECX, R12D						; ecx = y
	imul ECX, R15D						; ecx = y * stride
	mov r14d, R13D						; r14d = x
	imul r14d, 4						; r14d = x * 4 (bytesPerPixel)
	add ECX, r14d						; ecx = (y * stride) + (x * bytesPerPixel);
	
	; RCX = current pixel index

	; Getting the value of each color
	movzx r8d, BYTE PTR [RSI + RCX]			; Red
	movzx r9d, BYTE PTR [RSI + RCX + 1]		; Green
	movzx r14d, BYTE PTR [RSI + RCX + 2]	; Blue

	; Conversion to floating
	cvtsi2ss xmm0, r8d
	cvtsi2ss xmm1, r9d
	cvtsi2ss xmm2, r14d

	; Calculated from the formula

	movss xmm3, dword ptr [BLUE]			; xmm3 = 0.299
	mulss xmm0, xmm3					; xmm0 = xmm0 * xmm3

	movss xmm3, dword ptr [GREEN]		; xmm3 = 0.587
	mulss xmm1, xmm3					; xmm1 = xmm1 * xmm3

	movss xmm3, dword ptr [RED]			; xmm3 = 0.114
	mulss xmm2, xmm3					; xmm2 = xmm2 * xmm3

	; Sum of results
	addss xmm0, xmm1					; xmm0 = xmm0 + xmm1
	addss xmm0, xmm2					; xmm0 = xmm0 + xmm1 + xmm2

	; Conversion 
	cvttss2si r8d, xmm0					; r8d = (int) grayValue

    ; Creating a histogram
	xor r9, r9
	movzx r9, r8b
	shl r9, 2							; shift left ( * 4 )
	lock inc dword ptr [RDX + r9]		; increment

	; Saving the resulting color
	mov BYTE PTR [RDI + RCX], r8b
	mov BYTE PTR [RDI + RCX + 1], r8b 
	mov BYTE PTR [RDI + RCX + 2], r8b

	inc R13D							; x++
	jmp inner_loop						; back to inner loop

end_inner_loop:
	inc R12D							; y++
	jmp outer_loop						; back to outer loop

done:
	pop r15
	pop r14
	pop r13
	pop r12
	pop rbx
	pop rdi
	pop rsi
	pop rbp
	ret

ConvertToGrayScale endp


Thresholding proc
	mov rax, rdx
	push rbp
	mov rbp, rsp
	push rsi
	push rdi
	push rbx
	push r12
	push r13
	push r14
	push r15

	; Assigning parameters to constants
	mov RSI, RCX					; SourcePointer -> RSI
	mov RDI, RDX					; ResultPointer -> RDI
	mov EBX, R8D					; Width -> EBX
	mov RDX, R9						; Threshold value -> RDX
	mov R15D,  dword ptr [RBP + 48]	; stride -> R15D
	mov R10D,  dword ptr [RBP + 56]	; start -> R10D
	mov R11D, dword ptr  [RBP + 64]	; endRow -> R11D


	mov R12D, R10D					; StartRow->R12D | r12d=y
outer_loop:
	cmp R12D, R11D					; y==EndRow ?
	jge done

	xor R13D, R13D					; r13d=0 | r13d=x
inner_loop:
	cmp R13D, EBX					; r13d==width ?
	jge end_inner_loop				; >= go to end_inner_loop

	; Calculate the offset for each pixel
	mov ECX, R12D					; ecx = y
	imul ECX, R15D					; ecx = y * stride
	mov r14d, R13D					; r14d = x
	imul r14d, 4					; r14d = x * 4 (bytesPerPixel)
	add ECX, r14d					; ecx = (y * stride) + (x * bytesPerPixel);
	
	; RCX = current pixel index

	; Getting the value of each color
	movzx r8d, BYTE PTR [RSI + RCX]			; red
	movzx r9d, BYTE PTR [RSI + RCX + 1]		; green
	movzx r14d, BYTE PTR [RSI + RCX + 2]	; blue

	; Conversion to floating
	cvtsi2ss xmm0, r8d
	cvtsi2ss xmm1, r9d
	cvtsi2ss xmm2, r14d

	; Calculated from the formula

	movss xmm3, dword ptr [BLUE]			; xmm3 = 0.299
	mulss xmm0, xmm3						; xmm0 = xmm0 * xmm3

	movss xmm3, dword ptr [GREEN]			; xmm3 = 0.587
	mulss xmm1, xmm3						; xmm1 = xmm1 * xmm3

	movss xmm3, dword ptr [RED]				; xmm3 = 0.114
	mulss xmm2, xmm3						; xmm2 = xmm2 * xmm3
		
	; Sum of results
	addss xmm0, xmm1						; xmm0 = xmm0 + xmm1
	addss xmm0, xmm2						; xmm0 = xmm0 + xmm1 + xmm2

	; Conversion 
	cvttss2si r8d, xmm0						; r8d = (int) grayValue

	; Comparison with the threshold
	cmp r8d, edx
	jle less
	mov r8b,255
	jmp end_compare

less:
	mov r8b,0

	; Saving the resulting color
end_compare:
	mov BYTE PTR [RDI + RCX], r8b
	mov BYTE PTR [RDI + RCX + 1], r8b 
	mov BYTE PTR [RDI + RCX + 2], r8b
	mov BYTE PTR [RDI + RCX + 3], 255

	inc R13D								; x++	
	jmp inner_loop							; back to inner loop

end_inner_loop:
	inc R12D								; y++
	jmp outer_loop							; back to outer loop

done:
	pop r15
	pop r14
	pop r13
	pop r12
	pop rbx
	pop rdi
	pop rsi
	pop rbp
	ret

Thresholding endp


ThresholdingOnly proc
	mov rax, rdx
	push rbp
	mov rbp, rsp
	push rsi
	push rdi
	push rbx
	push r12
	push r13
	push r14
	push r15


	; Assigning parameters to constants
	mov RSI, RCX							; SourcePointer -> RSI
	mov RDI, RDX							; ResultPointer -> RDI
	mov EBX, R8D							; Width -> EBX
	mov RDX, R9								; Threshold value -> RDX
	mov R15D,  dword ptr [RBP + 48]			; stride -> R15D
	mov R10D,  dword ptr [RBP + 56]			; start -> R10D
	mov R11D, dword ptr  [RBP + 64]			; endRow -> R11D


	mov R12D, R10D							; StartRow->R12D | r12d=y
outer_loop:
	cmp R12D, R11D							; y==EndRow?
	jge done

	xor R13D, R13D							; r13d=0 | r13d=x
inner_loop:
	cmp R13D, EBX							; r13d==width?
	jge end_inner_loop						; >= go to end_inner_loop

	; Calculate the offset for each pixel
	mov ECX, R12D							; ecx = y
	imul ECX, R15D							; ecx = y * stride
	mov r14d, R13D							; r14d = x
	imul r14d, 4							; r14d = x * 4 (bytesPerPixel)
	add ECX, r14d							; ecx = (y * stride) + (x * bytesPerPixel);
	
	; RCX = current pixel index

	; Getting the value of color
	movzx r8d, BYTE PTR [RSI + RCX]

	; Comparison with the threshold
	cmp r8d, edx
	jle less
	mov r8b,255
	jmp end_compare

less:
	mov r8b,0

	; Saving the resulting color
end_compare:
	mov BYTE PTR [RDI + RCX], r8b
	mov BYTE PTR [RDI + RCX + 1], r8b
	mov BYTE PTR [RDI + RCX + 2], r8b
	mov BYTE PTR [RDI + RCX + 3], 255

	inc R13D								; x++
	jmp inner_loop							; go to inner loop

end_inner_loop:
	inc R12D								; y++
	jmp outer_loop							; go to outer loop

done:
	pop r15
	pop r14
	pop r13
	pop r12
	pop rbx
	pop rdi
	pop rsi
	pop rbp
	ret

ThresholdingOnly endp

end