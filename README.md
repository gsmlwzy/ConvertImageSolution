# ConvertImageSolution
 Generate Hex File


## Hex File Format

| :      | LL         | aaaa           | TT     | D..D   | CC   | 0x0D 0x0A |
| ------ | ---------- | -------------- | ------ | ------ | ---- | --------- |
| 行起始 | D..D段长度 | 下载地址低16位 | 功能码 | 数据段 | 校验 | 换行符    |



> | TT值 |                                              |                    |
> | ---- |----------------------------------------------| ------------------ |
> | 00   | 00 – 数据记录（Data Record）                       |                    |
> | 01   | 01 – 文件结束记录（End of FileRecord）               | 最后一行，文件结束 |
> | 02   | 02 – 扩展段地址记录（Extended Segment Address Record） |                    |
> | 03   | 03 – 开始段地址记录（Start Segment Address Record）   |                    |
> | 04   | 04 – 扩展线性地址记录（Extended Linear Address Record） | 下载地址的高16位   |
> | 05   | 05 – 开始线性地址记录（Start Linear Address Record）   | 程序入口地址       |