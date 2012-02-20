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
' * Class:		ObjectNode
' * Description:	Nodo RTF especializado que contiene la informaci�n de un objeto.
' * *****************************************************************************


Imports System.Text
Imports siaCodeCompiler.Net.Sgoliver.NRtfTree.Core
Imports System.Globalization

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Encapsula un nodo RTF de tipo Objeto (Palabra clave "\object")
		''' </summary>
		Public Class ObjectNode
			Inherits Net.Sgoliver.NRtfTree.Core.RtfTreeNode
			#Region "Atributos Privados"

			''' <summary>
			''' Array de bytes con la informaci�n del objeto.
			''' </summary>
			Private objdata As Byte()

			#End Region

			#Region "Constructores"

			''' <summary>
			''' Constructor de la clase ObjectNode.
			''' </summary>
			''' <param name="node">Nodo RTF del que se obtendr�n los datos de la imagen.</param>
			Public Sub New(node As RtfTreeNode)
				If node IsNot Nothing Then
					'Asignamos todos los campos del nodo
					Me.NodeKey = node.NodeKey
					Me.HasParameter = node.HasParameter
					Me.Parameter = node.Parameter
					Me.ParentNode = node.ParentNode
					Me.RootNode = node.RootNode
					Me.NodeType = node.NodeType

					Me.ChildNodes.Clear()
					Me.ChildNodes.AddRange(node.ChildNodes)

					'Obtenemos los datos del objeto como un array de bytes
					getObjectData()
				End If
			End Sub

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' Devuelve el tipo de objeto.
			''' </summary>
			Public ReadOnly Property ObjectType() As String
				Get
					If Me.SelectSingleChildNode("objemb") IsNot Nothing Then
						Return "objemb"
					End If
					If Me.SelectSingleChildNode("objlink") IsNot Nothing Then
						Return "objlink"
					End If
					If Me.SelectSingleChildNode("objautlink") IsNot Nothing Then
						Return "objautlink"
					End If
					If Me.SelectSingleChildNode("objsub") IsNot Nothing Then
						Return "objsub"
					End If
					If Me.SelectSingleChildNode("objpub") IsNot Nothing Then
						Return "objpub"
					End If
					If Me.SelectSingleChildNode("objicemb") IsNot Nothing Then
						Return "objicemb"
					End If
					If Me.SelectSingleChildNode("objhtml") IsNot Nothing Then
						Return "objhtml"
					End If
					If Me.SelectSingleChildNode("objocx") IsNot Nothing Then
						Return "objocx"
					Else
						Return ""
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve la clase del objeto.
			''' </summary>
			Public ReadOnly Property ObjectClass() As String
				Get
					'Formato: {\*\objclass Paint.Picture}

					Dim node As RtfTreeNode = Me.SelectSingleNode("objclass")

					If node IsNot Nothing Then
						Return node.NextSibling.NodeKey
					Else
						Return ""
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el grupo RTF que encapsula el nodo "\result" del objeto.
			''' </summary>
			Public ReadOnly Property ResultNode() As RtfTreeNode
				Get
					Dim node As RtfTreeNode = Me.SelectSingleNode("result")

					'Si existe el nodo "\result" recuperamos el grupo RTF superior.
					If node IsNot Nothing Then
						node = node.ParentNode
					End If

					Return node
				End Get
			End Property

			''' <summary>
			''' Devuelve una cadena de caracteres con el contenido del objeto en formato hexadecimal.
			''' </summary>
			Public ReadOnly Property HexData() As String
				Get
					Dim Text As String = ""

					'Buscamos el nodo "\objdata"
					Dim objdataNode As RtfTreeNode = Me.SelectSingleNode("objdata")

					'Si existe el nodo
					If objdataNode IsNot Nothing Then
						'Buscamos los datos en formato hexadecimal (�ltimo hijo del grupo de \objdata)
						Text = objdataNode.ParentNode.LastChild.NodeKey
					End If

					Return Text
				End Get
			End Property

			#End Region

			#Region "M�todos Publicos"

			''' <summary>
			''' Devuelve un array de bytes con el contenido del objeto.
			''' </summary>
			''' <returns>Array de bytes con el contenido del objeto.</returns>
			Public Function GetByteData() As Byte()
				Return objdata
			End Function

			#End Region

			#Region "M�todos Privados"

			''' <summary>
			''' Obtiene los datos binarios del objeto a partir de la informaci�n contenida en el nodo RTF.
			''' </summary>
			Private Sub getObjectData()
				'Formato: ( '{' \object (<objtype> & <objmod>? & <objclass>? & <objname>? & <objtime>? & <objsize>? & <rsltmod>?) ('{\*' \objdata (<objalias>? & <objsect>?) <data> '}') <result> '}' )

				Dim Text As String = ""

				If Me.FirstChild.NodeKey = "object" Then
					'Buscamos el nodo "\objdata"
					Dim objdataNode As RtfTreeNode = Me.SelectSingleNode("objdata")

					'Si existe el nodo
					If objdataNode IsNot Nothing Then
						'Buscamos los datos en formato hexadecimal (�ltimo hijo del grupo de \objdata)
						Text = objdataNode.ParentNode.LastChild.NodeKey

						Dim dataSize As Integer = Text.Length \ 2
						objdata = New Byte(dataSize - 1) {}

						Dim sbaux As New StringBuilder(2)

						For i As Integer = 0 To Text.Length - 1
							sbaux.Append(Text(i))

							If sbaux.Length = 2 Then
								objdata(i \ 2) = Byte.Parse(sbaux.ToString(), NumberStyles.HexNumber)
								sbaux.Remove(0, 2)
							End If
						Next
					End If
				End If
			End Sub

			#End Region
		End Class
	End Namespace
End Namespace
