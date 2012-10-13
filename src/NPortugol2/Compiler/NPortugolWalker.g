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
using System.Collections;
using NPortugol2.VirtualMachine;
}

@members{
	ModuleBuilder builder = new ModuleBuilder();
	
	bool inExpression;
	
	//public Dictionary<int, int> SourceMap { get { return emitter.SourceMap; } }
	
	//public bool DebugInfo {get {return emitter.DebugInfo;} set{emitter.DebugInfo = value;} }
}

public compile returns[Module module] : declare_function*
	{ return builder.Module;}
;

declare_function : ^(FUNC TYPE? ID function_param_list* ^(SLIST statement*))
	{builder.CreateFunction($TYPE, $ID.Token);}
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
	
param	: ^(t=TYPE i=ID) {builder.CreateFunctionParams($t.Token, $i.Token); };

declare_local 
	:  ^(VAR i+=ID*)  /*{ foreach(var item in $i) emitter.EmitVar(item.Token); }*/
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
	:  ^(RET plus_expression)/*{emitter.EmitRet($RET.Token);}*/
	;

// ##########################################################################################################################
// Expressions

plus_expression
: ^('+' plus_expression plus_expression) /*{emitter.EmitStackAdd();}*/
| ^('-' plus_expression plus_expression) /*{emitter.EmitStackSub();}*/
| ^('*' plus_expression plus_expression) /*{emitter.EmitStackPlus();}*/
| ^('/' plus_expression plus_expression) /*{emitter.EmitStackDiv();}*/
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
      a=ID /*{$value = $a.text; if (inExpression) emitter.EmitPush($value, $a.Token);}*/
    | a=INT /*{$value = int.Parse($a.text); if (inExpression) emitter.EmitPush($value, $a.Token);}*/
    | a=FLOAT /*{$value = float.Parse($a.text); if (inExpression) emitter.EmitPush($value, $a.Token);}*/
    | a=STRING /*{$value = $a.text; if (inExpression) emitter.EmitPush($value, $a.Token);}*/
    ;  