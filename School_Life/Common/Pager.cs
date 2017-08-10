using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 分页工具类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pager<T>
    {
        private const int PAGEITEMS = 10; //页码数
	    private int[] pageBar;//页码数组
	    private int currentPage = 1;//当前页
	    private int pageSize = 5;//每页记记录数
	    private List<T> list;//保存数据的集合
	    private int totalRecord;//总记录数
	    private int totalPage;//总页数
	    private int startIndex;//每页开始位置
	    private int endIndex;//每页结束位置
	    private int previousPage;//上一页
	    private int nextPage;//下一页
	
	    public int getCurrentPage() {
		    return currentPage;
	    }
	    public void setCurrentPage(int currentPage) {
		    this.currentPage = currentPage;
	    }
	    public int getPageSize() {
		    return pageSize;
	    }
	    public void setPageSize(int pageSize) {
		    this.pageSize = pageSize;
	    }
	    public List<T> getList() {
		    return list;
	    }
	    public void setList(List<T> list) {
		    this.list = list;
	    }
	    public int getTotalRecord() {
		    return totalRecord;
	    }
	    public void setTotalRecord(int totalRecord) {
		    this.totalRecord = totalRecord;
	    }
	    public int getTotalPage() {
		    int result = this.getTotalRecord() % this.getPageSize(); 
		    if (result != 0) {
			    this.totalPage = this.getTotalRecord() / this.getPageSize() + 1;
		    }else{
			    this.totalPage = this.getTotalRecord() / this.getPageSize();
		    }
		    return totalPage;
	    }
	    public int getStartIndex() {
		    this.startIndex = (this.getCurrentPage() - 1) * this.getPageSize();
		    return startIndex;
	    }
	    public void setStartIndex(int startIndex) {
		    if (startIndex < 0) {
			    this.startIndex = 0;
		    }else{
			    this.startIndex = startIndex;
		    }
	    }
	    public int getEndIndex() {
		    int end = this.getCurrentPage() * this.getPageSize();
		    if (end > this.getTotalRecord()) {
			    end = this.getTotalRecord();
		    }
		    this.endIndex = end;
		    return endIndex;//5
	    }
	    public void setEndIndex(int endIndex) {// -2	curr:1  p:5
		    if (endIndex < 0) {
			    int end = this.getCurrentPage() * this.getPageSize(); 
			    if (end > this.getTotalRecord()) {
				    end = this.getTotalRecord();
			    }
			    this.endIndex = end;
		    }else{
			    this.endIndex = endIndex;
		    }
		
	    }
	    public int getPreviousPage() {
		    int previous = this.getCurrentPage() - 1;
		    if (previous <= 0) {
			    this.previousPage = 1;
		    }else{
			    this.previousPage = previous;
		    }
		    return previousPage;
	    }
	
	    public int getNextPage(){
		    int next = this.getCurrentPage() + 1;
		    if (next > this.getTotalPage()) {
			    next = this.getTotalPage();
		    }
		    this.nextPage = next;
		    return this.nextPage;
	    }
	
	    /**
	     * 获取页码数量
	     * @return
	     */
	    public int[] getPageBar(){
		    int start;
		    int end;
		    int[] pageBar = null;
		    if(this.getTotalPage() < PAGEITEMS){
			    pageBar = new int[this.getTotalPage()];
			    start = 1;
			    end = this.getTotalPage();
		    }else{
			    pageBar = new int[PAGEITEMS];
			    start = this.getCurrentPage() - (PAGEITEMS/2-1);
			    end = this.getCurrentPage() + PAGEITEMS/2;
			    if(start < 1){
				    start = 1;
				    end = PAGEITEMS;
			    }
			    if(end > this.getTotalPage()){
				    start = this.getTotalPage() - PAGEITEMS - 1;
				    end = this.getTotalPage();
			    }
		    }
		
		    int index = 0;
		    for (int i = start; i <= end; i++) {
			    pageBar[index++] = i;
		    }
		    this.pageBar = pageBar;
		    return this.pageBar;
	    }
    }
}
