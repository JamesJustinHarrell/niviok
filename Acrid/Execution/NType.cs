using System;
using Acrid.NodeTypes;

namespace Acrid.Execution {

//xxx what is this?
enum TypeType {
	INTERFACE, MANUAL
}

//xxx move
public interface IType {}

//xxx conflicts with System.Type
public class NType : IType {
	TypeType _type;
	IInterface _face;

	//used by Bridge.stdn_any
	public NType() {
		//xxx should I use BUILTIN instead?
		_type = TypeType.MANUAL;
	}
	
	public NType(IInterface face) {
		_type = TypeType.INTERFACE;
		_face = face;
	}
	
	public NType(IWorker worker) {
		/* xxx
		if candowncast(worker, Bridge.stdn_Type)
			worker = downcast(worker, Bridge.stdn_Type)
		else
			throw
		face = worker.face
		if face == Bridge.stdn_Interface
			...
		elif face == Bridge.stdn_Nullable
			...
		elif face == ManualType
			...
		...
		*/
		//xxx for now, worker must be an interface
		_type = TypeType.INTERFACE;
		_face = Bridge.toNativeInterface(worker);
	}
}

} //namespace
