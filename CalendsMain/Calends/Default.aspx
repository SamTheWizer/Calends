<%@ Page Title="Start Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calends._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container{
            text-align: center;
        }
    </style>
    <div class="jumbotron">
        <h1>Calends</h1>
        <p class="lead">Welcome to Calends! Please select an option below</p>
    </div>

   <!-- <div class="row">
        <div class="col-md-4">
            <h2><a href="">New Project</a></h2>
            <p>Start a new calends project</p>
            <h2><a href="">Open Existing Project</a></h2>
            <p>Manage a calends project</p>
        </div>
        <div class="col-md-8">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
    </div>-->
    <div class ="container">
        <div class ="row">
            <div class ="col-md-4">
                </div>
            <div class ="col-md-4">
                    <h2><a runat="server" href="~/Account/Login">Log In</a></h2>
                </div>
            <div class ="col-md-4">
                </div>
        </div>
        <div class ="row">
            <div class ="col-md-4">
                </div>
            <div class ="col-md-4">
                <h2><a runat="server" href="~/Account/Register">Register</a></h2>
               </div>
            <div class ="col-md-4">
                </div>
        </div>
</asp:Content>
