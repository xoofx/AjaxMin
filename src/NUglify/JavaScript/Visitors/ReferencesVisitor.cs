﻿// Copyright 2014 Microsoft Corporation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using NUglify.JavaScript.Syntax;

namespace NUglify.JavaScript.Visitors
{
    public class ReferencesVisitor : TreeVisitor
    {
	    bool m_hasReference;
	    LookupExpression m_lookup;

	    ReferencesVisitor(LookupExpression lookup)
        {
            m_lookup = lookup;
        }

        public static bool References(AstNode node, LookupExpression lookup)
        {
            if (node == null || lookup == null)
            {
                return false;
            }

            var visitor = new ReferencesVisitor(lookup);
            node.Accept(visitor);
            return visitor.m_hasReference;
        }

        public override void Visit(LookupExpression node)
        {
            // see if this node is equivalent to the target node.
            // if it is, then the tree has at least one reference to the target lookup
            if (node != null && node.IsEquivalentTo(m_lookup))
            {
                m_hasReference = true;
            }
        }
    }
}
