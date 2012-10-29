tree grammar NPortugolWalker;

options{
tokenVocab=NPortugol;
language=CSharp3;
ASTLabelType=CommonTree;
backtrack=true;
k=3;
}

@namespace{NPortugol2.Compiler}

@header{
using System;
using NPortugol2.Core;
using System.Reflection.Emit;
}

@members{
	CodeEmitter emitter = new CodeEmitter();
	
	bool inExpression;
	
	//public Dictionary<int, int> SourceMap { get { return emitter.SourceMap; } }
	
	//public bool DebugInfo {get {return emitter.DebugInfo;} set{emitter.DebugInfo = value;} }
}

public compile returns[Module module] : declare_function*
	{ return emitter.Module;}
;

declare_function : ^(FUNC TYPE? ID function_param_list* ^(SLIST statement*))
	{emitter.CreateFunction($TYPE, $ID.Token);}
;
		
statement: declare_local
	| if_stat 
	| for_stat
	| function_call 
	| assign_var
	| return_stat
	| asm_code
	;
	
	
function_param_list    
	:	 ^(PARAM param*)
	;	 
	
param	: ^(t=TYPE i=ID) {emitter.CreateFunctionArg($t.Token, $i.Token); };

	
declare_local 
	: ^(VAR t=local_var more_var[t]*) /*{emitter.DeclareLocal(t, $i); }*/
	;

local_var returns[Type value]
	: 
	  ^(t=TYPE i=ID a=atom?) {$value = emitter.DeclareLocal($t.Token, $i.Token, a);}
	;
	
more_var [Type value]
	:	
	^(i=ID a=atom?) { emitter.DeclareLocal(value, $i.Token, a); }
	;	

if_stat
	:  ^(SJMP ^(LEXP logic_expression) ^(SLIST statement* /*{emitter.EmitIf(true);}*/ senao_stat))
	|  ^(JMP ^(LEXP logic_expression) ^(SLIST statement*))
	/*{emitter.EmitIf(false);}*/
	;
	
senao_stat
	: ^(SLIST statement*)
	   /*{emitter.EmitElse();}*/
	;		
	
	
for_stat: ^(LOOP a=assign_var i=INT ^(SLIST statement* ))	
	| ^(LOOP DEC a=assign_var i=INT ^(SLIST statement*)) 
	| ^(LOOP a=assign_var i=ID ^(SLIST statement* ))	
	| ^(LOOP DEC a=assign_var i=ID ^(SLIST statement*))  	
	;
		
function_call   
	:	 ^(CALL ID function_arg_list*)
		/*{emitter.EmitCall($ID.Token);}*/
	;
	
property_call 
	:	 ^(PCALL o=ID p=ID)
		/*{emitter.EmitPropCall($o.Token, $p.Token);}*/
	;	
	
method_call     
	:	 ^(MCALL o=ID p=ID function_arg_list*)
		/*{emitter.EmitMethodCall($o.Token, $p.Token);}	*/
	;		

function_arg_list    
	
	:	^(ARG plus_expression*)
	;	
	
	
asm_code:	^(ASM s+=STRING*)
		/*{emitter.EmitAsmCode($s);}*/
        ;
	

assign_var returns[string id]
    :  
      ^(ASGN ID a=atom) /*{$id = $ID.text; emitter.EmitAssign($ID.Token, $a.value);}*/
    | ^(ASGN ^(AR INT) ID plus_expression) /*{emitter.EmitPop($ID.Token, int.Parse($INT.text));}  */    
    | ^(ASGN ^(AR i=ID) p=ID plus_expression) /*{emitter.EmitPop($p.Token, $i.text);}   */       
    | ^(ASGN ID plus_expression) /*{emitter.EmitPop($ID.Token);}*/ 
    | ^(ASGN ID 'nulo') /*{emitter.EmitAssign($ID.Token, null);}*/ 
    | ^(ASGN ID 'falso') /*{emitter.EmitAssign($ID.Token, false);} */
    | ^(ASGN ID 'verdadeiro') /*{emitter.EmitAssign($ID.Token, true);}   */      
    | ^(ASGN ID l=INT r=INT) /*{emitter.EmitAssign($ID.Token, int.Parse($l.text), int.Parse($r.text));}*/
    | ^(ASGN ID ^(ILIST i=INT*)) /*{emitter.EmitAssignArray($ID.Token, $i);}*/
    ;		

return_stat
	:  ^(RET plus_expression) {emitter.Emit(OpCodes.Ret);}
	;

// ##########################################################################################################################
// Expressions

plus_expression
@init { inExpression = true; }
@after { inExpression = false; }
: ^('+' plus_expression plus_expression) {emitter.Emit(OpCodes.Add);}
| ^('-' plus_expression plus_expression) {emitter.Emit(OpCodes.Sub);}
| ^('*' plus_expression plus_expression) {emitter.Emit(OpCodes.Mul);}
| ^('/' plus_expression plus_expression) {emitter.Emit(OpCodes.Div);}
| ^(INDEX INT) ID /*{emitter.EmitPush($ID.text, int.Parse($INT.text));}*/
| ^(INDEX i2=ID) i1=ID /*{emitter.EmitPush($i1.text, $i2.text);}*/
| function_call
| method_call
| property_call
| atom
;

logic_expression
	:	  ^('<' plus_expression plus_expression) /*{emitter.EmitLessExp();}*/
	|	  ^('>' plus_expression plus_expression) /*{emitter.EmitGreaterExp();}*/
	|	  ^('<=' plus_expression plus_expression) /*{emitter.EmitLessEqExp();}*/
	|	  ^('>=' plus_expression plus_expression) /*{emitter.EmitGreaterEqExp();}*/
	|	  ^('==' plus_expression plus_expression) /*{emitter.EmitEqualsExp();}*/
	|	  ^('!=' plus_expression plus_expression)/* {emitter.EmitNotEqExp();}*/
	|	  ^('e' plus_expression plus_expression)
	|	  ^('ou' plus_expression plus_expression)						
	| plus_expression
	;	

//###########################################################################################################################
    
atom returns[object value]: 
      a=ID {$value = $a.text; if (inExpression) emitter.EmitLoadVar((string)$value, $a.Token);}
    | a=INT {$value = int.Parse($a.text); if (inExpression) emitter.EmitLdcI4((int)$value, $a.Token);}
    | a=FLOAT {$value = float.Parse($a.text); if (inExpression) emitter.EmitLdcR4((float)$value, $a.Token);}
    | a=STRING {$value = $a.text; if (inExpression) emitter.EmitLdstr((string)$value, $a.Token);}
    ;  