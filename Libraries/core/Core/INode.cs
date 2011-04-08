﻿/*

Copyright Robert Vesse 2009-10
rvesse@vdesign-studios.com

------------------------------------------------------------------------

This file is part of dotNetRDF.

dotNetRDF is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

dotNetRDF is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with dotNetRDF.  If not, see <http://www.gnu.org/licenses/>.

------------------------------------------------------------------------

dotNetRDF may alternatively be used under the LGPL or MIT License

http://www.gnu.org/licenses/lgpl.html
http://www.opensource.org/licenses/mit-license.php

If these licenses are not suitable for your intended use please contact
us at the above stated email address to discuss alternative
terms.

*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;

namespace VDS.RDF
{

    /// <summary>
    /// Node Type Values
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// A Blank Node
        /// </summary>
        Blank = 0, 
        /// <summary>
        /// A Uri Node
        /// </summary>
        Uri = 1, 
        /// <summary>
        /// A Literal Node
        /// </summary>
        Literal = 2,
        /// <summary>
        /// A Graph Literal Node
        /// </summary>
        GraphLiteral = 3,
        /// <summary>
        /// A Variable Node (currently only used in N3)
        /// </summary>
        Variable = 4
    }

    /// <summary>
    /// Interface for Nodes
    /// </summary>
    public interface INode 
        : IComparable<INode>, IComparable<IBlankNode>, IComparable<IGraphLiteralNode>, IComparable<ILiteralNode>,
          IComparable<IUriNode>, IComparable<IVariableNode>,
          IEquatable<INode>, IEquatable<IBlankNode>, IEquatable<IGraphLiteralNode>, IEquatable<ILiteralNode>,
          IEquatable<IUriNode>, IEquatable<IVariableNode>
    {
        /// <summary>
        /// Nodes have a Type
        /// </summary>
        /// <remarks>Primarily provided so can do quick integer comparison to see what type of Node you have without having to do actual full blown Type comparison</remarks>
        NodeType NodeType
        {
            get;
        }

        /// <summary>
        /// Nodes belong to a Graph
        /// </summary>
        IGraph Graph
        {
            get;
        }

        /// <summary>
        /// The Graph a Node belongs to may have a Uri
        /// </summary>
        Uri GraphUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets/Sets whether the Nodes Hash Code collides with other Nodes in the Graph
        /// </summary>
        /// <remarks>
        /// Designed for internal use only, exposed via the Interface in order to simplify implementation.  For Triples the equivalent method is protected internal since we pass a concrete class as the parameter and can do this but without switching the entire API to use <see cref="BaseNode">BaseNode</see> as the type for Nodes the same is not possible and this is not a change we wish to make to the API as it limits extensibility
        /// </remarks>
        bool Collides
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the String representation of the Node formatted with the given Node formatter
        /// </summary>
        /// <param name="formatter">Formatter</param>
        /// <returns></returns>
        String ToString(INodeFormatter formatter);

        /// <summary>
        /// Gets the String representation of the Node formatted with the given Node formatter
        /// </summary>
        /// <param name="formatter">Formatter</param>
        /// <param name="segment">Triple Segment</param>
        /// <returns></returns>
        String ToString(INodeFormatter formatter, TripleSegment segment);
    }

    /// <summary>
    /// Interface for URI Nodes
    /// </summary>
    public interface IUriNode : INode
    {
        Uri Uri
        {
            get;
        }
    }

    public interface IBlankNode : INode
    {
        String InternalID
        {
            get;
        }
    }

    public interface ILiteralNode : INode
    {
        String Value
        {
            get;
        }

        String Language
        {
            get;
        }

        Uri DataType
        {
            get;
        }
    }

    public interface IGraphLiteralNode : INode
    {
        IGraph SubGraph
        {
            get;
        }
    }

    public interface IVariableNode : INode
    {
        String VariableName
        {
            get;
        }
    }
}
