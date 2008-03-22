//a Desal node

using System.Collections.Generic;

interface INode {
	string typeName {get;}
	ICollection<INode> childNodes {get;}
	HashSet<Identifier> identikeyDependencies {get;}
}
