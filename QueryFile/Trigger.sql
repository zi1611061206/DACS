--trigger
create trigger UpdateBillInfo
on dbo.ChiTietHoaDon for insert, update
as 
begin
	declare @maHoaDon int
	declare @maBan int

	select @maHoaDon=maHoaDon from inserted
	select @maBan=maBan from dbo.HoaDon where maHoaDon=@maHoaDon and trangThai = 0

	update dbo.Ban set trangThai = 1 where maBan=@maBan
end
go
-----------------------------------------------------

-----------------------------------------------------
create trigger UpdateBill
on dbo.HoaDon for update 
as
begin
	declare @maBanCanChuyen int
	declare @maBanChuyenDen int
	declare @maHoaDon int
	declare @maBan int
	declare @dem int
	
	select @maHoaDon=maHoaDon from inserted
	select @maBan=maBan from dbo.HoaDon where maHoaDon=@maHoaDon
	select @maBanChuyenDen=maBan from inserted
	select @maBanCanChuyen=maBan from deleted
	select @dem =count(*) from dbo.HoaDon where maBan=@maBan and trangThai=0

	update dbo.Ban set trangThai=0 where maBan=@maBanCanChuyen
	update dbo.Ban set trangThai=1 where maBan=@maBanChuyenDen
	
	if(@dem=0)
		update dbo.Ban set trangThai=0 where maBan=@maBan
end
go
-----------------------------------------------------
create trigger DeleteBillInfo
on dbo.ChiTietHoaDon for delete
as
begin
	declare @maHoaDon int
	select @maHoaDon = maHoaDon from deleted

	declare @maBan int
	select @maBan = maBan from dbo.HoaDon where maHoaDon = @maHoaDon

	declare @dem int =0
	select @dem = count(*) from dbo.ChiTietHoaDon as c, dbo.HoaDon as h where c.maHoaDon=h.maHoaDon and c.maHoaDon=@maHoaDon and h.trangThai = 0

	if(@dem = 0)
		update dbo.Ban set trangThai = 0 where maBan = @maBan
end
go
-----------------------------------------------------
create trigger UpdatePriceFollowMenu
on dbo.ChiTietHoaDon for insert, update
as 
begin
	declare @maDoUong int
	declare @maHoaDon int
	declare @giaBan float
	declare @soLuong int

	select @maDoUong=maDoUong, @maHoaDon=maHoaDon, @soLuong=cast(soLuong as float) from inserted
	select @giaBan=giaBan from dbo.ThucDon where maDoUong=@maDoUong

	declare @thanhTien float
	set @thanhTien=@giaBan*@soLuong
	update dbo.ChiTietHoaDon set thanhTien=@thanhTien where maDoUong=@maDoUong and maHoaDon=@maHoaDon
end
go