create proc CheckLogin
@username varchar(10),@password nvarchar(1000)
as 
begin
	select * from dbo.TaiKhoan where TenDangNhap = @username and MatKhau = @password
end
go
---------------------------------------------------------------------------------------
CREATE proc GetBillByTableID
@maban int
as
begin
	select t.TenDoUong, c.SoLuong, t.GiaBan, c.ThanhTien 
	from ChiTietHoaDon as c, HoaDon as h, ThucDon as t 
	where c.MaHoaDon=h.MaHoaDon and c.MaDoUong=t.MaDoUong and h.MaBan=@maban and h.TrangThai = 0
end
go
---------------------------------------------------------------------------------------
CREATE proc GetDisplayRecord
@thoigiandau datetime, @thoigiancuoi datetime, @pagenumber int, @max_number_rows_ofpage int
as
begin
	declare @number_rows_select int = @max_number_rows_ofpage*@pagenumber
	declare @number_rows_except int = @max_number_rows_ofpage*(@pagenumber-1)
	
	;with revenue as (select h.MAHOADON as [Mã hóa đơn], b.TENBAN as [Tên bàn], c.THOIGIANKET as [Thời gian thanh toán], h.GIAMGIA as [Giảm giá], h.TONGTIEN as [Tổng tiền]
	from dbo.HOADON as h, dbo.CHITIETHOADON as c, dbo.BAN as b
	where h.MAHOADON=c.MAHOADON and h.MABAN=b.MABAN and c.THOIGIANKET >= @thoigiandau and c.THOIGIANKET <= @thoigiancuoi and h.TRANGTHAI = 1
	group by h.MAHOADON, TENBAN, THOIGIANKET, GIAMGIA, TONGTIEN)
	
	select count(*) from (select top (@number_rows_select) * from revenue except select top (@number_rows_except) * from revenue) as #temp
end
go
---------------------------------------------------------------------------------------
CREATE proc GetRevenue
@thoigiandau datetime, @thoigiancuoi datetime, @pagenumber int, @max_number_rows_ofpage int
as
begin
	declare @number_rows_select int = @max_number_rows_ofpage*@pagenumber
	declare @number_rows_except int = @max_number_rows_ofpage*(@pagenumber-1)
	
	;with revenue as (select h.mahoadon as [Mã hóa đơn], b.tenban as [Tên bàn], c.thoigianket as [Thời gian thanh toán], h.giamgia as [Giảm giá], h.tongtien as [Tổng tiền]
	from dbo.HOADON as h, dbo.CHITIETHOADON as c, dbo.BAN as b
	where h.mahoadon=c.mahoadon and h.maban=b.maban and c.thoigianket >= @thoigiandau and c.thoigianket <= @thoigiancuoi and h.trangthai = 1
	group by h.mahoadon, tenban, thoigianket, giamgia, tongtien)
	
	select top (@number_rows_select) * from revenue except select top (@number_rows_except) * from revenue
end
go
---------------------------------------------------------------------------------------
CREATE proc GetRevenueRecordNum
@thoigiandau datetime,
@thoigiancuoi datetime
as
begin
	select count(*)	
	from dbo.HOADON as h, dbo.CHITIETHOADON as c, dbo.BAN as b
	where h.MAHOADON=c.MAHOADON and h.MABAN=b.MABAN and c.THOIGIANKET >= @thoigiandau and c.THOIGIANKET <= @thoigiancuoi and h.TRANGTHAI = 1
end
go
---------------------------------------------------------------------------------------
create proc GetTableList
as 
begin
	select * from dbo.ban
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListCategory
as
begin
	select MADANHMUC as [Mã danh mục], TENDANHMUC as [Tên danh mục] from dbo.DANHMUC
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListEmployees
as 
begin
	select n.MANHANVIEN as [Mã] ,n.TENDANGNHAP as [Tên đăng nhập], n.HOTEN as [Họ tên], c.TENCHUCVU as [Chức vụ], n.GIOITINH as [Giới tính], n.CHUNGMINHNHANDAN as [CMND], n.DIACHI as [Địa chỉ], n.SODIENTHOAI as [SĐT], n.ANHDAIDIEN as [Ảnh] 
	from dbo.NHANVIEN as n, dbo.CHUCVU as c
	where c.MACHUCVU=n.MACHUCVU
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListMaterial
as
begin
	select n.MANGUYENLIEU as [Mã nguyên liệu], c.TENNGUYENLIEU as [Tên nguyên liệu], nc.TENNHACUNGCAP as [Nhà cung cấp], c.DONGIA as [Đơn giá], c.DONVI as [Đơn vị], c.SOLUONGTON as [Số lượng], n.TINHTRANG as [Tình trạng], c.GHICHU as [Ghi chú] from dbo.NGUYENLIEU as n, dbo.CHITIETNGUYENLIEU as c, dbo.NHACUNGCAP as nc where n.MANGUYENLIEU=c.MANGUYENLIEU and c.MANHACUNGCAP=nc.MANHACUNGCAP
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListMenu
as 
begin
	select  [Mã] = t.MADOUONG, [Tên đồ uống]=t.TENDOUONG, [Tên danh mục]=d.TENDANHMUC, [Giá bán]=t.GIABAN, [Tình trạng]=t.TINHTRANG, [Hình ảnh]=t.HINHANH from dbo.THUCDON as t, dbo.DANHMUC as d where t.MADANHMUC=d.MADANHMUC
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListPosition
as 
begin
	select MACHUCVU as [Mã], TENCHUCVU as [Chức vụ]
	from dbo.CHUCVU
end
go
---------------------------------------------------------------------------------------
CREATE proc LoadListSupplier
as 
begin
	select MANHACUNGCAP as [Mã], TENNHACUNGCAP as [Tên nhà cung cấp], DIACHI as [Địa chỉ], SODIENTHOAI as [SĐT], EMAIL as [Email], GHICHU as [Ghi chú]
	from dbo.NHACUNGCAP
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchCategory
@tendanhmuc nvarchar(50)
as
begin
	select MADANHMUC as [Mã danh mục], TENDANHMUC as [Tên danh mục] from dbo.DANHMUC where dbo.fuConvertToUnsign1(TENDANHMUC) like N'%'+dbo.fuConvertToUnsign1( @tendanhmuc )+'%'
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchEmployees
@hoten varchar(10)
as 
begin
	select n.MANHANVIEN as [Mã] ,n.TENDANGNHAP as [Tên đăng nhập], n.HOTEN as [Họ tên], c.TENCHUCVU as [Chức vụ], n.GIOITINH as [Giới tính], n.CHUNGMINHNHANDAN as [CMND], n.DIACHI as [Địa chỉ], n.SODIENTHOAI as [SĐT], n.ANHDAIDIEN as [Ảnh] 
	from dbo.NHANVIEN as n, dbo.CHUCVU as c
	where c.MACHUCVU=n.MACHUCVU and dbo.fuConvertToUnsign1(HOTEN) like  N'%'+dbo.fuConvertToUnsign1( @hoten )+'%'
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchMaterial
@tennguyenlieu nvarchar(50)
as
begin
	select n.MANGUYENLIEU as [Mã nguyên liệu], c.TENNGUYENLIEU as [Tên nguyên liệu], nc.TENNHACUNGCAP as [Nhà cung cấp], c.DONGIA as [Đơn giá], c.DONVI as [Đơn vị], c.SOLUONGTON as [Số lượng], n.TINHTRANG as [Tình trạng], c.GHICHU as [Ghi chú]
	from dbo.NGUYENLIEU as n, dbo.CHITIETNGUYENLIEU as c, dbo.NHACUNGCAP as nc 
	where n.MANGUYENLIEU=c.MANGUYENLIEU and c.MANHACUNGCAP=nc.MANHACUNGCAP and dbo.fuConvertToUnsign1(TENNGUYENLIEU) like N'%'+dbo.fuConvertToUnsign1( @tennguyenlieu )+'%'
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchMenu
@tendouong nvarchar(10)
as 
begin
	select  [Mã] = t.MADOUONG, [Tên đồ uống]=t.TENDOUONG, [Tên danh mục]=d.TENDANHMUC, [Giá bán]=t.GIABAN, [Tình trạng]=t.TINHTRANG, [Hình ảnh]=t.HINHANH
	from dbo.THUCDON as t, dbo.DANHMUC as d 
	where t.MADANHMUC=d.MADANHMUC and dbo.fuConvertToUnsign1(TENDOUONG) like N'%'+dbo.fuConvertToUnsign1( @tendouong )+'%'
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchPosition
@tenchucvu nvarchar(10)
as 
begin
	select  MACHUCVU as [Mã], TENCHUCVU as [Chức vụ]
	from dbo.CHUCVU 
	where dbo.fuConvertToUnsign1(TENCHUCVU) like N'%'+dbo.fuConvertToUnsign1( @tenchucvu )+'%'
end
go
---------------------------------------------------------------------------------------
CREATE proc SearchSupplier
@tennhacungcap nvarchar(50)
as
begin
	select MANHACUNGCAP as [Mã], TENNHACUNGCAP as [Tên nhà cung cấp], DIACHI as [Địa chỉ], SODIENTHOAI as [SĐT], EMAIL as [Email], GHICHU as [Ghi chú]
	from dbo.NHACUNGCAP
	where dbo.fuConvertToUnsign1(TENNHACUNGCAP) like N'%'+dbo.fuConvertToUnsign1( @tennhacungcap )+'%'
end
go
---------------------------------------------------------------------------------------
create proc UpdateIntoExistedBill
@mahoadon int,
@madouong int,
@soluong int
as
begin
	declare @existed int 
	declare @countexisted int = 1
	--kiem tra do uong da ton tai trong hoa don do chua?
	select @existed = mahoadon, @countexisted = soluong  
	from dbo.chitiethoadon 
	where mahoadon=@mahoadon and madouong=@madouong
	--neu da co thi cap nhat lai soluong
	if(@existed>0)
	begin
		declare @newcount int = @countexisted + @soluong
		if(@newcount>0) --neu so luong ban dau + so luong them vao ma van con duong thi cap nhat lai soluong
		begin
			update dbo.chitiethoadon 
			set soluong = @countexisted + @soluong
			where (madouong=@madouong and mahoadon=@mahoadon)
		end
		else --nguoc lai, xoa dong do uong da co khoi cthd
		begin
			delete dbo.chitiethoadon
			where (mahoadon=@mahoadon and madouong=@madouong)
		end
	end
	--nguoc lai, insert do uong vao nhu 1 do uong moi
	else
	begin
		insert into dbo.chitiethoadon (mahoadon,madouong,soluong) values ( @mahoadon ,  @madouong , @soluong)
	end
end
go
---------------------------------------------------------------------------------------
CREATE proc UpdatePassword
@tendangnhap varchar(10),
@matkhau nvarchar(1000),
@matkhaumoi nvarchar(1000)
as
begin
	declare @pw_is_right int=0

	select @pw_is_right=count(*) from dbo.TAIKHOAN where tendangnhap=@tendangnhap and matkhau= @matkhau

	if(@pw_is_right>=1)
	begin
		update dbo.TAIKHOAN set matkhau = @matkhaumoi  where tendangnhap= @tendangnhap
	end
end
go
---------------------------------------------------------------------------------------
