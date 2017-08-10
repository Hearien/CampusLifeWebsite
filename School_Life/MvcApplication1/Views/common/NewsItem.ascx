<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Model.News>>" %>
<style>
    .newsitem ul li span a{display:inline-block;max-width:240px;overflow:hidden;font-size:15px;line-height:25px;}
    .newsitem ul li span span{background-color:#ff0000;color:#fff;line-height:10px;vertical-align:top;}
</style>
<div class="row col-md-12 newsitem">
    <ul>
        <%for (int i = 0; i < 5; i++){%>

            <li>
            <%
              var title = Model.ElementAt(i).newstitle;
              if (title.Length > 15)
              {
                  title = Model.ElementAt(i).newstitle.Substring(0, 13);
             %> 
                <span class="col-md-8"><a href="/News/Details?newsid=<%=Html.Encode(Model.ElementAt(i).newsid) %>" title="<%=Html.Encode(Model.ElementAt(i).newstitle) %>"><%=Html.Encode(title)%></a><span class="badge">NEW</span></span>
            <%}else{ %>
                <span class="col-md-8"><a href="/News/Details?newsid=<%=Html.Encode(Model.ElementAt(i).newsid) %>"><%=Html.Encode(Model.ElementAt(i).newstitle)%></a><span class="badge">NEW</span></span>
            <%} %>
                <span class="col-md-4"><%=Html.Encode(Model.ElementAt(i).createtime)%></span>
            </li>

        <%} %>
    </ul>
</div>