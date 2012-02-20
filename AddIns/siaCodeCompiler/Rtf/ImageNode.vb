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
' * Class:		ImageNode
' * Description:	Nodo RTF especializado que contiene la informaci�n de una imagen.
' * *****************************************************************************


Imports System.Text
Imports siaCodeCompiler.Net.Sgoliver.NRtfTree.Core
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Encapsula un nodo RTF de tipo Imagen (Palabra clave "\pict")
		''' </summary>
		Public Class ImageNode
			Inherits Net.Sgoliver.NRtfTree.Core.RtfTreeNode
			#Region "Atributos privados"

			''' <summary>
			''' Array de bytes con la informaci�n de la imagen.
			''' </summary>
			Private data As Byte()

			#End Region

			#Region "Constructores"

			''' <summary>
			''' Constructor de la clase ImageNode.
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

					'Obtenemos los datos de la imagen como un array de bytes
					getImageData()
				End If
			End Sub

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' Devuelve una cadena de caracteres con el contenido de la imagen en formato hexadecimal.
			''' </summary>
			Public ReadOnly Property HexData() As String
				Get
					Return Me.SelectSingleChildNode(RtfNodeType.Text).NodeKey
				End Get
			End Property

			''' <summary>
			''' Devuelve el formato original de la imagen.
			''' </summary>
			Public ReadOnly Property ImageFormat() As System.Drawing.Imaging.ImageFormat
				Get
					If Me.SelectSingleChildNode("jpegblip") IsNot Nothing Then
						Return System.Drawing.Imaging.ImageFormat.Jpeg
					ElseIf Me.SelectSingleChildNode("pngblip") IsNot Nothing Then
						Return System.Drawing.Imaging.ImageFormat.Png
					ElseIf Me.SelectSingleChildNode("emfblip") IsNot Nothing Then
						Return System.Drawing.Imaging.ImageFormat.Emf
					ElseIf Me.SelectSingleChildNode("wmetafile") IsNot Nothing Then
						Return System.Drawing.Imaging.ImageFormat.Wmf
					ElseIf Me.SelectSingleChildNode("dibitmap") IsNot Nothing OrElse Me.SelectSingleChildNode("wbitmap") IsNot Nothing Then
						Return System.Drawing.Imaging.ImageFormat.Bmp
					Else
						Return Nothing
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el ancho de la imagen (en twips).
			''' </summary>
			Public ReadOnly Property Width() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("picw")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el alto de la imagen (en twips).
			''' </summary> 
			Public ReadOnly Property Height() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("pich")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el ancho objetivo de la imagen (en twips).
			''' </summary>
			Public ReadOnly Property DesiredWidth() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("picwgoal")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve el alto objetivo de la imagen (en twips).
			''' </summary>
			Public ReadOnly Property DesiredHeight() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("pichgoal")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve la escala horizontal de la imagen, en porcentaje.
			''' </summary>
			Public ReadOnly Property ScaleX() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("picescalex")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			''' <summary>
			''' Devuelve la escala vertical de la imagen, en porcentaje.
			''' </summary>
			Public ReadOnly Property ScaleY() As Integer
				Get
					Dim node As RtfTreeNode = Me.SelectSingleChildNode("picescaley")

					If node IsNot Nothing Then
						Return node.Parameter
					Else
						Return -1
					End If
				End Get
			End Property

			#End Region

			#Region "Metodos Publicos"

			''' <summary>
			''' Devuelve un array de bytes con el contenido de la imagen.
			''' </summary>
			''' <return>Array de bytes con el contenido de la imagen.</return>
			Public Function GetByteData() As Byte()
				Return data
			End Function

			''' <summary>
			''' Guarda una imagen a fichero con el formato original.
			''' </summary>
			''' <param name="filePath">Ruta del fichero donde se guardar� la imagen.</param>
			Public Sub SaveImage(filePath As String)
				If data IsNot Nothing Then
					Dim stream As New MemoryStream(data, 0, data.Length)

					'Escribir a un fichero cualquier tipo de imagen
					Dim bitmap As New Bitmap(stream)
					bitmap.Save(filePath, Me.ImageFormat)
				End If
			End Sub

			''' <summary>
			''' Guarda una imagen a fichero con un formato determinado indicado como par�metro.
			''' </summary>
			''' <param name="filePath">Ruta del fichero donde se guardar� la imagen.</param>
			''' <param name="format">Formato con el que se escribir� la imagen.</param>
			Public Sub SaveImage(filePath As String, format As System.Drawing.Imaging.ImageFormat)
				If data IsNot Nothing Then
					Dim stream As New MemoryStream(data, 0, data.Length)

					'System.Drawing.Imaging.Metafile metafile = new System.Drawing.Imaging.Metafile(stream);

					'Escribir directamente el array de bytes a un fichero ".jpg"
					'FileStream fs = new FileStream("c:\\prueba.jpg", FileMode.CreateNew);
					'BinaryWriter w = new BinaryWriter(fs);
					'w.Write(image,0,imageSize);
					'w.Close();
					'fs.Close();

					'Escribir a un fichero cualquier tipo de imagen
					Dim bitmap As New Bitmap(stream)
					bitmap.Save(filePath, format)
				End If
			End Sub

			#End Region

			#Region "Metodos privados"

			''' <summary>
			''' Obtiene los datos de la imagen a partir de la informaci�n contenida en el nodo RTF.
			''' </summary>
			Private Sub getImageData()
				'Formato 1 (Word 97-2000): {\*\shppict {\pict\jpegblip <datos>}}{\nonshppict {\pict\wmetafile8 <datos>}}
				'Formato 2 (Wordpad)     : {\pict\wmetafile8 <datos>}

				Dim Text As String = ""

				If Me.FirstChild.NodeKey = "pict" Then
					Text = Me.SelectSingleChildNode(RtfNodeType.Text).NodeKey

					Dim dataSize As Integer = Text.Length \ 2
					data = New Byte(dataSize - 1) {}

					Dim sbaux As New StringBuilder(2)

					For i As Integer = 0 To Text.Length - 1
						sbaux.Append(Text(i))

						If sbaux.Length = 2 Then
							data(i \ 2) = Byte.Parse(sbaux.ToString(), NumberStyles.HexNumber)
							sbaux.Remove(0, 2)
						End If
					Next
				End If
			End Sub

			#End Region
		End Class
	End Namespace
End Namespace
