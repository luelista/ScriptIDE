'*******************************************************************************
' *   This file is part of NRtfTree Library.
' *
' *   NRtfTree Library is free software; you can redistribute it and/or modify
' *   it under the terms of the GNU Lesser General Public License as published by
' *   the Free Software Foundation; either version 3 of the License, or
' *   (at your option) any later version.
' *
' *   NRtfTree Library is distributed in the hope that it will be useful,
' *   but WITHOUT ANY WARRANTY; without even the implied warranty of
' *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' *   GNU Lesser General Public License for more details.
' *
' *   You should have received a copy of the GNU Lesser General Public License
' *   along with this program. If not, see <http://www.gnu.org/licenses/>.
' *******************************************************************************


'*******************************************************************************
' * Library:		NRtfTree
' * Version:     v0.3.0
' * Date:		02/09/2007
' * Copyright:   2007 Salvador Gomez
' * E-mail:      sgoliver.net@gmail.com
' * Home Page:	http://www.sgoliver.net
' * SF Project:	http://nrtftree.sourceforge.net
' *				http://sourceforge.net/projects/nrtftree
' * Class:		RtfTreeNode
' * Description:	Nodo RTF de la representaci�n en �rbol de un documento.
' * *****************************************************************************


Imports System.Collections
Imports System.IO
Imports System.Text

Namespace Net.Sgoliver.NRtfTree
	Namespace Core
		''' <summary>
		''' Nodo RTF de la representaci�n en �rbol de un documento.
		''' </summary>
		Public Class RtfTreeNode
			#Region "Atributos Privados"

			''' <summary>
			''' Tipo de nodo.
			''' </summary>
			Private type As RtfNodeType
			''' <summary>
			''' Palabra clave / S�mbolo de Control / Texto.
			''' </summary>
			Private key As String
			''' <summary>
			''' Indica si la palabra clave o s�mbolo de Control tiene par�metro.
			''' </summary>
			Private hasParam As Boolean
			''' <summary>
			''' Par�metro de la palabra clave o s�mbolo de Control.
			''' </summary>
			Private param As Integer
			''' <summary>
			''' Nodos hijos del nodo actual.
			''' </summary>
			Private children As RtfNodeCollection
			''' <summary>
			''' Nodo padre del nodo actual.
			''' </summary>
			Private parent As RtfTreeNode
			''' <summary>
			''' Nodo ra�z del documento.
			''' </summary>
			Private root As RtfTreeNode
			''' <summary>
			''' �rbol Rtf al que pertenece el nodo
			''' </summary>
			Private m_tree As RtfTree

			#End Region

			#Region "Constructores P�blicos"

			''' <summary>
			''' Constructor de la clase RtfTreeNode. Crea un nodo sin inicializar.
			''' </summary>
			Public Sub New()
				children = New RtfNodeCollection()

				Me.type = RtfNodeType.None

					' Inicializados por defecto 

					'this.param = 0;
					'this.hasParam = false;
					'this.parent = null;
					'this.root = null;
				Me.key = ""
			End Sub

			''' <summary>
			''' Constructor de la clase RtfTreeNode. Crea un nodo de un tipo concreto.
			''' </summary>
			''' <param name="nodeType">Tipo del nodo que se va a crear.</param>
			Public Sub New(nodeType As RtfNodeType)
				children = New RtfNodeCollection()

				Me.type = nodeType

					' Inicializados por defecto 

					'this.param = 0;
					'this.hasParam = false;
					'this.parent = null;
					'this.root = null;
				Me.key = ""
			End Sub

			''' <summary>
			''' Constructor de la clase RtfTreeNode. Crea un nodo especificando su tipo, palabra clave y par�metro.
			''' </summary>
			''' <param name="type">Tipo del nodo.</param>
			''' <param name="key">Palabra clave o s�mbolo de Control.</param>
			''' <param name="hasParameter">Indica si la palabra clave o el s�mbolo de Control va acompa�ado de un par�metro.</param>
			''' <param name="parameter">Par�metro del la palabra clave o s�mbolo de Control.</param>
			Public Sub New(type As RtfNodeType, key As String, hasParameter As Boolean, parameter As Integer)
				children = New RtfNodeCollection()

				Me.type = type
				Me.key = key
				Me.hasParam = hasParameter

					' Inicializados por defecto 

					'this.parent = null;
					'this.root = null;
				Me.param = parameter
			End Sub

			#End Region

			#Region "Constructor Privado"

			''' <summary>
			''' Constructor privado de la clase RtfTreeNode. Crea un nodo a partir de un token del analizador l�xico.
			''' </summary>
			''' <param name="token">Token RTF devuelto por el analizador l�xico.</param>
			Friend Sub New(token As RtfToken)
				children = New RtfNodeCollection()

				Me.type = DirectCast(token.Type, RtfNodeType)
				Me.key = token.Key
				Me.hasParam = token.HasParameter

					' Inicializados por defecto 

					'this.parent = null;
					'this.root = null;
				Me.param = token.Parameter
			End Sub

			#End Region

			#Region "M�todos P�blicos"

			''' <summary>
			''' A�ade un nodo al final de la lista de hijos.
			''' </summary>
			''' <param name="newNode">Nuevo nodo a a�adir.</param>
			Public Sub AppendChild(newNode As RtfTreeNode)
				If newNode IsNot Nothing Then
					'Se asigna como nodo padre el nodo actual
					newNode.parent = Me

					'Se actualizan las propiedades Root y Tree del nuevo nodo y sus posibles hijos
					updateNodeRoot(newNode)

					'Se a�ade el nuevo nodo al final de la lista de nodos hijo
					children.Add(newNode)
				End If
			End Sub

			''' <summary>
			''' Inserta un nuevo nodo en una posici�n determinada de la lista de hijos.
			''' </summary>
			''' <param name="index">Posici�n en la que se insertar� el nodo.</param>
			''' <param name="newNode">Nuevo nodo a insertar.</param>
			Public Sub InsertChild(index As Integer, newNode As RtfTreeNode)
				If newNode IsNot Nothing AndAlso index >= 0 AndAlso index <= children.Count Then
					'Se asigna como nodo padre el nodo actual
					newNode.parent = Me

					'Se actualizan las propiedades Root y Tree del nuevo nodo y sus posibles hijos
					updateNodeRoot(newNode)

					'Se a�ade el nuevo nodo al final de la lista de nodos hijo
					children.Insert(index, newNode)
				End If
			End Sub

			''' <summary>
			''' Elimina un nodo de la lista de hijos.
			''' </summary>
			''' <param name="index">Indice del nodo a eliminar.</param>
			Public Sub RemoveChild(index As Integer)
				'Se elimina el i-�simo hijo
				children.RemoveAt(index)
			End Sub

			''' <summary>
			''' Elimina un nodo de la lista de hijos.
			''' </summary>
			''' <param name="node">Nodo a eliminar.</param>
			Public Sub RemoveChild(node As RtfTreeNode)
				'Se busca el nodo a eliminar
				Dim index As Integer = children.IndexOf(node)

				'Se elimina el i-�simo hijo
				children.RemoveAt(index)
			End Sub

			''' <summary>
			''' Realiza una copia exacta del nodo actual.
			''' </summary>
			''' <param name="cloneChildren">Si este par�metro recibe el valor true se clonar�n tambi�n todos los nodos hijo del nodo actual.</param>
			''' <returns>Devuelve una copia exacta del nodo actual.</returns>
			Public Function CloneNode(cloneChildren As Boolean) As RtfTreeNode
				Dim clon As New RtfTreeNode()

				clon.key = Me.key
				clon.hasParam = Me.hasParam
				clon.param = Me.param
				clon.parent = Me.parent
				clon.root = Me.root
				clon.type = Me.type

				'Si cloneChildren=false se copia directamente la lista de hijos
				If Not cloneChildren Then
					clon.children = Me.children
				Else
					'En caso contrario se clonan tambi�n cada uno de los hijos, propagando el par�metro cloneChildren=true
					clon.children = New RtfNodeCollection()

					For Each child As RtfTreeNode In Me.children
						clon.children.Add(child.CloneNode(True))
					Next
				End If

				Return clon
			End Function

			''' <summary>
			''' Indica si el nodo actual tiene nodos hijos.
			''' </summary>
			''' <returns>Devuelve true si el nodo actual tiene alg�n nodo hijo.</returns>
			Public Function HasChildNodes() As Boolean
				Return (children.Count <> 0)
			End Function

			''' <summary>
			''' Devuelve el primer nodo de la lista de nodos hijos del nodo actual cuya palabra clave es la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Primer nodo de la lista de nodos hijos del nodo actual cuya palabra clave es la indicada como par�metro.</returns>
			Public Function SelectSingleChildNode(keyword As String) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).key = keyword Then
						node = children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo de la lista de nodos hijos del nodo actual cuyo tipo es el indicado como par�metro.
			''' </summary>
			''' <param name="nodeType">Tipo de nodo buscado.</param>
			''' <returns>Primer nodo de la lista de nodos hijos del nodo actual cuyo tipo es el indicado como par�metro.</returns>
			Public Function SelectSingleChildNode(nodeType As RtfNodeType) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).type = nodeType Then
						node = children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo de la lista de nodos hijos del nodo actual cuya palabra clave y par�metro son los indicados como par�metros.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <param name="param">Par�metro buscado.</param>
			''' <returns>Primer nodo de la lista de nodos hijos del nodo actual cuya palabra clave y par�metro son los indicados como par�metros.</returns>
			Public Function SelectSingleChildNode(keyword As String, param As Integer) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).key = keyword AndAlso children(i).param = param Then
						node = children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo del �rbol, a partir del nodo actual, cuyo tipo es el indicado como par�metro.
			''' </summary>
			''' <param name="nodeType">Tipo del nodo buscado.</param>
			''' <returns>Primer nodo del �rbol, a partir del nodo actual, cuyo tipo es el indicado como par�metro.</returns>
			Public Function SelectSingleNode(nodeType As RtfNodeType) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).type = nodeType Then
						node = children(i)
						found = True
					Else
						node = children(i).SelectSingleNode(nodeType)

						If node IsNot Nothing Then
							found = True
						End If
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo del �rbol, a partir del nodo actual, cuya palabra clave es la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Primer nodo del �rbol, a partir del nodo actual, cuya palabra clave es la indicada como par�metro.</returns>
			Public Function SelectSingleNode(keyword As String) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).key = keyword Then
						node = children(i)
						found = True
					Else
						node = children(i).SelectSingleNode(keyword)

						If node IsNot Nothing Then
							found = True
						End If
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo del �rbol, a partir del nodo actual, cuya palabra clave y par�metro son los indicados como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <param name="param">Par�metro buscado.</param>
			''' <returns>Primer nodo del �rbol, a partir del nodo actual, cuya palabra clave y par�metro son ls indicados como par�metro.</returns>
			Public Function SelectSingleNode(keyword As String, param As Integer) As RtfTreeNode
				Dim i As Integer = 0
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < children.Count AndAlso Not found
					If children(i).key = keyword AndAlso children(i).param = param Then
						node = children(i)
						found = True
					Else
						node = children(i).SelectSingleNode(keyword, param)

						If node IsNot Nothing Then
							found = True
						End If
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve todos los nodos, a partir del nodo actual, cuya palabra clave es la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Colecci�n de nodos, a partir del nodo actual, cuya palabra clave es la indicada como par�metro.</returns>
			Public Function SelectNodes(keyword As String) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.key = keyword Then
						nodes.Add(node)
					End If

					nodes.AddRange(node.SelectNodes(keyword))
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve todos los nodos, a partir del nodo actual, cuyo tipo es el indicado como par�metro.
			''' </summary>
			''' <param name="nodeType">Tipo del nodo buscado.</param>
			''' <returns>Colecci�n de nodos, a partir del nodo actual, cuyo tipo es la indicado como par�metro.</returns>
			Public Function SelectNodes(nodeType As RtfNodeType) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.type = nodeType Then
						nodes.Add(node)
					End If

					nodes.AddRange(node.SelectNodes(nodeType))
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve todos los nodos, a partir del nodo actual, cuya palabra clave y par�metro son los indicados como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <param name="param">Par�metro buscado.</param>
			''' <returns>Colecci�n de nodos, a partir del nodo actual, cuya palabra clave y par�metro son los indicados como par�metro.</returns>
			Public Function SelectNodes(keyword As String, param As Integer) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.key = keyword AndAlso node.param = param Then
						nodes.Add(node)
					End If

					nodes.AddRange(node.SelectNodes(keyword, param))
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve todos los nodos de la lista de nodos hijos del nodo actual cuya palabra clave es la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Colecci�n de nodos de la lista de nodos hijos del nodo actual cuya palabra clave es la indicada como par�metro.</returns>
			Public Function SelectChildNodes(keyword As String) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.key = keyword Then
						nodes.Add(node)
					End If
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve todos los nodos de la lista de nodos hijos del nodo actual cuyo tipo es el indicado como par�metro.
			''' </summary>
			''' <param name="nodeType">Tipo del nodo buscado.</param>
			''' <returns>Colecci�n de nodos de la lista de nodos hijos del nodo actual cuyo tipo es el indicado como par�metro.</returns>
			Public Function SelectChildNodes(nodeType As RtfNodeType) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.type = nodeType Then
						nodes.Add(node)
					End If
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve todos los nodos de la lista de nodos hijos del nodo actual cuya palabra clave y par�metro son los indicados como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <param name="param">Par�metro buscado.</param>
			''' <returns>Colecci�n de nodos de la lista de nodos hijos del nodo actual cuya palabra clave y par�metro son los indicados como par�metro.</returns>
			Public Function SelectChildNodes(keyword As String, param As Integer) As RtfNodeCollection
				Dim nodes As New RtfNodeCollection()

				For Each node As RtfTreeNode In children
					If node.key = keyword AndAlso node.param = param Then
						nodes.Add(node)
					End If
				Next

				Return nodes
			End Function

			''' <summary>
			''' Devuelve el primer nodo hermano del actual cuya palabra clave es la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Primer nodo hermano del actual cuya palabra clave es la indicada como par�metro.</returns>
			Public Function SelectSibling(keyword As String) As RtfTreeNode
				Dim par As RtfTreeNode = Me.parent
				Dim curInd As Integer = par.ChildNodes.IndexOf(Me)

				Dim i As Integer = curInd + 1
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < par.children.Count AndAlso Not found
					If par.children(i).key = keyword Then
						node = par.children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo hermano del actual cuyo tipo es el indicado como par�metro.
			''' </summary>
			''' <param name="nodeType">Tpo de nodo buscado.</param>
			''' <returns>Primer nodo hermano del actual cuyo tipo es el indicado como par�metro.</returns>
			Public Function SelectSibling(nodeType As RtfNodeType) As RtfTreeNode
				Dim par As RtfTreeNode = Me.parent
				Dim curInd As Integer = par.ChildNodes.IndexOf(Me)

				Dim i As Integer = curInd + 1
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < par.children.Count AndAlso Not found
					If par.children(i).type = nodeType Then
						node = par.children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve el primer nodo hermano del actual cuya palabra clave y par�metro son los indicados como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <param name="param">Par�metro buscado.</param>
			''' <returns>Primer nodo hermano del actual cuya palabra clave y par�metro son los indicados como par�metro.</returns>
			Public Function SelectSibling(keyword As String, param As Integer) As RtfTreeNode
				Dim par As RtfTreeNode = Me.parent
				Dim curInd As Integer = par.ChildNodes.IndexOf(Me)

				Dim i As Integer = curInd + 1
				Dim found As Boolean = False
				Dim node As RtfTreeNode = Nothing

				While i < par.children.Count AndAlso Not found
					If par.children(i).key = keyword AndAlso par.children(i).param = param Then
						node = par.children(i)
						found = True
					End If

					i += 1
				End While

				Return node
			End Function

			''' <summary>
			''' Devuelve una representaci�n del nodo donde se indica su tipo, clave, indicador de par�metro y valor de par�metro
			''' </summary>
			''' <returns>Cadena de caracteres del tipo [TIPO, CLAVE, IND_PARAMETRO, VAL_PARAMETRO]</returns>
			Public Overloads Overrides Function ToString() As String
				Return "[" & Convert.ToString(Me.type) & ", " & Me.key & ", " & Me.hasParam & ", " & Me.param & "]"
			End Function

			#End Region

			#Region "Metodos Privados"

			''' <summary>
			''' Obtiene el Texto RTF a partir de la representaci�n en �rbol del nodo actual.
			''' </summary>
			''' <returns>Texto RTF del nodo.</returns>
			Private Function getRtf() As String
				Dim res As String = ""

				Dim enc As Encoding = Me.m_tree.GetEncoding()

				res = getRtfInm(Me, Nothing, enc)

				Return res
			End Function

			''' <summary>
			''' M�todo auxiliar para obtener el Texto RTF del nodo actual a partir de su representaci�n en �rbol.
			''' </summary>
			''' <param name="curNode">Nodo actual del �rbol.</param>
			''' <param name="prevNode">Nodo anterior tratado.</param>
			''' <param name="enc">Codificaci�n del documento.</param>
			''' <returns>Texto en formato RTF del nodo.</returns>
			Private Function getRtfInm(curNode As RtfTreeNode, prevNode As RtfTreeNode, enc As Encoding) As String
				Dim res As New StringBuilder("")

				If curNode.NodeType = RtfNodeType.Root Then
					res.Append("")
				ElseIf curNode.NodeType = RtfNodeType.Group Then
					res.Append("{")
				Else
					If curNode.NodeType <> RtfNodeType.Text Then
						res.Append("\")
					Else
						'curNode.NodeType == RtfNodeType.Text
						If prevNode Is Nothing OrElse prevNode.NodeType = RtfNodeType.Control Then
							res.Append("")
						Else
							'antNode.NodeType == RtfNodeType.KEYWORD
							res.Append(" ")
						End If
					End If

					AppendEncoded(res, curNode.NodeKey, enc)

					If curNode.HasParameter Then
						If curNode.NodeType = RtfNodeType.Keyword Then
							res.Append(Convert.ToString(curNode.Parameter))
						ElseIf curNode.NodeType = RtfNodeType.Control Then
							'Si es un caracter especial como las vocales acentuadas
							If curNode.NodeKey = "'" Then
								res.Append(GetHexa(curNode.Parameter))
							End If
						End If
					End If
				End If

				'Se obtienen los nodos hijos
				Dim children As RtfNodeCollection = curNode.ChildNodes

				For i As Integer = 0 To children.Count - 1
					Dim node As RtfTreeNode = children(i)

					If i > 0 Then
						res.Append(getRtfInm(node, children(i - 1), enc))
					Else
						res.Append(getRtfInm(node, Nothing, enc))
					End If
				Next

				If curNode.NodeType = RtfNodeType.Group Then
					res.Append("}")
				End If

				Return res.ToString()
			End Function

			''' <summary>
			''' Concatena dos cadenas utilizando la codificaci�n del documento.
			''' </summary>
			''' <param name="res">Cadena original.</param>
			''' <param name="s">Cadena a a�adir.</param>
			''' <param name="enc">Codificaci�n del documento.</param>
			Private Sub AppendEncoded(res As StringBuilder, s As String, enc As Encoding)
				'Contributed by Jan Stuchl�k

				For i As Integer = 0 To s.Length - 1
					Dim code As Integer = [Char].ConvertToUtf32(s, i)

					If code >= 128 OrElse code < 32 Then
						res.Append("\'")
						Dim bytes As Byte() = enc.GetBytes(New Char() {s(i)})
						res.Append(GetHexa(bytes(0)))
					Else
						res.Append(s(i))
					End If
				Next
			End Sub

			''' <summary>
			''' Obtiene el c�digo hexadecimal de un entero.
			''' </summary>
			''' <param name="code">N�mero entero.</param>
			''' <returns>C�digo hexadecimal del entero pasado como par�metro.</returns>
			Private Function GetHexa(code As Integer) As String
				'Contributed by Jan Stuchl�k

				Dim hexa As String = Convert.ToString(code, 16)

				If hexa.Length = 1 Then
					hexa = "0" & hexa
				End If

				Return hexa
			End Function

			''' <summary>
			''' Actualiza las propiedades Root y Tree de un nodo (y sus hijos) con las del nodo actual.
			''' </summary>
			''' <param name="node">Nodo a actualizar.</param>
			Private Sub updateNodeRoot(node As RtfTreeNode)
				'Se asigna el nodo ra�z del documento
				node.root = Me.root

				'Se asigna el �rbol propietario del nodo
				node.tree = Me.m_tree

				'Se actualizan recursivamente los hijos del nodo actual
				For Each nod As RtfTreeNode In node.children
					updateNodeRoot(nod)
				Next
			End Sub

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' Devuelve el nodo ra�z del �rbol del documento.
			''' </summary>
			''' <remarks>
			''' �ste no es el nodo ra�z del �rbol, sino que se trata simplemente de un nodo ficticio  de tipo ROOT del que parte el resto del �rbol RTF.
			''' Tendr� por tanto un solo nodo hijo de tipo GROUP, raiz real del �rbol.
			''' </remarks>
			Public Property RootNode() As RtfTreeNode
				Get
					Return root
				End Get
				Set
					root = value
				End Set
			End Property

			''' <summary>
			''' Devuelve el nodo padre del nodo actual.
			''' </summary>
			Public Property ParentNode() As RtfTreeNode
				Get
					Return parent
				End Get
				Set
					parent = value
				End Set
			End Property

			''' <summary>
			''' Devuelve el �rbol Rtf al que pertenece el nodo.
			''' </summary>
			Public Property Tree() As RtfTree
				Get
					Return m_tree
				End Get
				Set
					m_tree = value
				End Set
			End Property

			''' <summary>
			''' Devuelve el tipo del nodo actual.
			''' </summary>
			Public Property NodeType() As RtfNodeType
				Get
					Return type
				End Get
				Set
					type = value
				End Set
			End Property

			''' <summary>
			''' Devuelve la palabra clave, s�mbolo de Control o Texto del nodo actual.
			''' </summary>
			Public Property NodeKey() As String
				Get
					Return key
				End Get
				Set
					key = value
				End Set
			End Property

			''' <summary>
			''' Indica si el nodo actual tiene par�metro asignado.
			''' </summary>
			Public Property HasParameter() As Boolean
				Get
					Return hasParam
				End Get
				Set
					hasParam = value
				End Set
			End Property

			''' <summary>
			''' Devuelve el par�metro asignado al nodo actual.
			''' </summary>
			Public Property Parameter() As Integer
				Get
					Return param
				End Get
				Set
					param = value
				End Set
			End Property

			''' <summary>
			''' Devuelve la colecci�n de nodos hijo del nodo actual.
			''' </summary>
			Public ReadOnly Property ChildNodes() As RtfNodeCollection
				Get
					Return children
				End Get
			End Property

			''' <summary>
			''' Devuelve el primer nodo hijo cuya palabra clave sea la indicada como par�metro.
			''' </summary>
			''' <param name="keyword">Palabra clave buscada.</param>
			''' <returns>Primer nodo hijo cuya palabra clave sea la indicada como par�metro. En caso de no existir se devuelve null.</returns>
			Public Default ReadOnly Property Item(keyword As String) As RtfTreeNode
				Get
					Return Me.SelectSingleChildNode(keyword)
				End Get
			End Property

			''' <summary>
			''' Devuelve el primer nodo hijo del nodo actual.
			''' </summary>
			Public ReadOnly Property FirstChild() As RtfTreeNode
				Get
					If children.Count > 0 Then
						Return children(0)
					Else
						Return Nothing
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el �ltimo nodo hijo del nodo actual.
			''' </summary>
			Public ReadOnly Property LastChild() As RtfTreeNode
				Get
					If children.Count > 0 Then
						Return children(children.Count - 1)
					Else
						Return Nothing
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el nodo hermano siguiente del nodo actual (Dos nodos son hermanos si tienen el mismo nodo padre [ParentNode]).
			''' </summary>
			Public ReadOnly Property NextSibling() As RtfTreeNode
				Get
					Dim currentIndex As Integer = parent.children.IndexOf(Me)

					If parent.children.Count > currentIndex + 1 Then
						Return parent.children(currentIndex + 1)
					Else
						Return Nothing
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el nodo hermano anterior del nodo actual (Dos nodos son hermanos si tienen el mismo nodo padre [ParentNode]).
			''' </summary>
			Public ReadOnly Property PreviousSibling() As RtfTreeNode
				Get
					Dim currentIndex As Integer = parent.children.IndexOf(Me)

					If currentIndex > 0 Then
						Return parent.children(currentIndex - 1)
					Else
						Return Nothing
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el c�digo RTF del nodo actual y todos sus nodos hijos.
			''' </summary>
			Public ReadOnly Property Rtf() As String
				Get
					Return getRtf()
				End Get
			End Property

			#End Region
		End Class
	End Namespace
End Namespace
