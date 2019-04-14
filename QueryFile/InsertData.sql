use QUANLYCAPHE
go

insert into dbo.NhaCungCap(tennhacungcap, diachi, sodienthoai, email) values (N'Trung Nguyên', N'Bình Thạnh', '0123456789', 'abc@gmail.com')
insert into dbo.NhaCungCap(tennhacungcap, diachi, sodienthoai, email) values (N'Monster', N'Bình Chánh', '0123456788', 'monster@gmail.com')
go

insert into dbo.nguyenlieu (tinhtrang) values ( 0)
insert into dbo.nguyenlieu (tinhtrang) values ( 0)
insert into dbo.nguyenlieu (tinhtrang) values ( 0)
insert into dbo.nguyenlieu (tinhtrang) values ( 0)
go

insert into dbo.ChiTietNguyenLieu (manguyenlieu,tennguyenlieu,MaNhaCungCap,dongia,donvi,SoLuongTon) values (1, N'Cafe rang xay', 1,120000,'kg',10)
insert into dbo.ChiTietNguyenLieu (manguyenlieu,tennguyenlieu,MaNhaCungCap,dongia,donvi,SoLuongTon) values (2, N'Cafe chồn', 1,180000,'kg',10)
insert into dbo.ChiTietNguyenLieu (manguyenlieu,tennguyenlieu,MaNhaCungCap,dongia,donvi,SoLuongTon) values (3, N'Siro blue', 2,180000,'chai',1)
go

insert into dbo.danhmuc (tendanhmuc) values (N'Cà phê')
insert into dbo.danhmuc (tendanhmuc) values (N'Sinh tố')
insert into dbo.danhmuc (tendanhmuc) values (N'Soda')
insert into dbo.danhmuc (tendanhmuc) values (N'Nước giải khát')
insert into dbo.danhmuc (tendanhmuc) values (N'Yaourt')
go

insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (1,N'Cà phê sữa đá',12000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (1,N'Cà phê đen nóng',12000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (2,N'Sinh tố cà rốt',25000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (3,N'Soda blue',20000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (4,N'Sting',15000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (4,N'Ô long',15000)
insert into dbo.thucdon (madanhmuc,tendouong,giaban) values (5,N'Yaourt đá',18000)
go

insert into dbo.ban (tenban) values (N'Bàn 1')
insert into dbo.ban (tenban) values (N'Bàn 2')
insert into dbo.ban (tenban) values (N'Bàn 3')
insert into dbo.ban (tenban) values (N'Bàn 4')
insert into dbo.ban (tenban) values (N'Bàn 5')
insert into dbo.ban (tenban) values (N'Bàn 6')
insert into dbo.ban (tenban) values (N'Bàn 7')
insert into dbo.ban (tenban) values (N'Bàn 8')
insert into dbo.ban (tenban) values (N'Bàn 9')
insert into dbo.ban (tenban) values (N'Bàn 10')
insert into dbo.ban (tenban) values (N'Bàn 11')
insert into dbo.ban (tenban) values (N'Bàn 12')
insert into dbo.ban (tenban) values (N'Bàn 13')
insert into dbo.ban (tenban) values (N'Bàn 14')
insert into dbo.ban (tenban) values (N'Bàn 15')
go

insert into dbo.chucvu (tenchucvu) values (N'Quản lý')
insert into dbo.chucvu (tenchucvu) values (N'Thu ngân')
go 

insert into dbo.TaiKhoan(tendangnhap) values ('zi')
insert into dbo.TaiKhoan(tendangnhap,matkhau) values ('iz',123)
go

insert into dbo.NhanVien(tendangnhap,hoten,machucvu,ChungMinhNhanDan,diachi,SoDienThoai) values ('zi',N'Nguyễn Văn A',1,'123456789',N'Bình Thạnh','0987654321')
insert into dbo.NhanVien(tendangnhap,hoten,machucvu,ChungMinhNhanDan,diachi,SoDienThoai) values ('iz',N'Nguyễn Văn B',2,'123456780',N'Bình Thạnh','0987654321')
go

delete NHANVIEN
delete TAIKHOAN
delete CHUCVU
delete CHUCNANG
delete PHANQUYEN
delete CHITIETTHUCDON
delete DANHMUC
delete CHITIETNGUYENLIEU
delete NGUYENLIEU
delete NHACUNGCAP
delete CHITIETHOADON
delete HOADON
delete THUCDON