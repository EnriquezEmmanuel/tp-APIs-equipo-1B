<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProducto.aspx.cs" Inherits="TP_Web_Promo_WEB.ListadoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/Personalizar.css" rel="stylesheet" />
    <h2 style="text-align: center;">Productos</h2>
    <hr />
    <div class="row">
        <asp:Repeater ID="rptArticulos" runat="server" OnItemDataBound="rptArticulos_ItemDataBound"> <%--Creo el evento--%>
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card mb-3">
                        <!-- Carrusel de imágenes -->
                        <div id='carousel_<%# Eval("Id") %>' class="carousel slide">
                            <div class="carousel-inner">
                                <asp:Repeater ID="rptImagenes" runat="server">
                                    <ItemTemplate> <!-- Se muestra una tarjeta por producto en una grilla de 3 columnas, con un carrusel de imágenes y sus datos dentro.-->
                                        <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'> <%--Este Repeater muestra todas las imágenes del artículo en un carrusel, marcando la primera como "active" para que Bootstrap la cargue como imagen inicial.--%>
                                            <img src='<%# Eval("Url") %>' class="imagen" alt="Imagen del artículo" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target='#carousel_<%# Eval("Id") %>' data-bs-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Anterior</span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target='#carousel_<%# Eval("Id") %>' data-bs-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Siguiente</span>
                            </button>
                        </div>

                        <div class="card-body"> <%--Mostrás todos los datos del producto usando Eval, que vincula a las propiedades del objeto Articulo.--%>
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p><strong>Marca:</strong> <%# Eval("Marca") %></p>
                            <p><strong>Categoría:</strong> <%# Eval("Categoria") %></p>
                            <p><strong>Precio:</strong> $<%# Eval("Precio", "{0:F2}") %></p>
                            <asp:Button class="btn btn-primary" ID="BtnSelecionar" runat="server" OnClick="BtnSelecionar_Click" Text="Selecionar" CommandArgument='<%# Eval("Id") %>'/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Label id="Temporal" Text="" runat="server" />
</asp:Content>
