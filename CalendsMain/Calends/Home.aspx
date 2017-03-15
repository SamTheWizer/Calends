<%@ Page Title="Homepage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calends._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container{
            text-align: center;
        }
    </style>
    <div class="jumbotron">
        <h1>Calends</h1>
        <p class="lead">Welcome User! Please select an option below</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2><a href="">New Project</a></h2>
            <p>Start a new calends project</p>
            <h2><a href="">Open Existing Project</a></h2>
            <p>Manage a calends project</p>
        </div>
        <div class="col-md-8">
             
            AddingAjaxPanel Here

        </div>
    </div>
</asp:Content>
