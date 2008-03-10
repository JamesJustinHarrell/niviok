//a Desal node

using System.Collections.Generic;

interface INode {
	string typeName {get;}
	ICollection<INode> children {get;}
	HashSet<Identifier> identikeyDependencies {get;}
}
