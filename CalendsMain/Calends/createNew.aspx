<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="createNew.aspx.cs" Inherits="Calends.createNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
        <h1>Create a New Project</h1>
        <p class="lead">Please fill in the fields below</p>
    </div>

    <div class ="pan">
      <div class ="row">
        <div class ="col-md-4">
            <h3>Name of the Project </h3>
            <asp:Textbox ID ="ProjectName" TextMode="SingleLine" runat="server"/> 
             <h3>Start Date</h3>
             <asp:Textbox ID ="StartDate" TextMode="DateTime" runat="server"/>
            <h3>End Date</h3>
            <asp:Textbox ID ="EndDate" TextMode="DateTime" runat="server"/>
            <h3>Block Size Parameter</h3>
            <asp:Textbox ID ="BlockSize" TextMode="Number" runat="server"/>
         </div>
         <div class ="col=md-8">
            <h3>Members</h3>
            <asp:Textbox ID ="Members" TextMode="MultiLine" Columns="100" Rows="20" runat="server"/>
        </div>
      </div>
        <div>





</asp:Content>
