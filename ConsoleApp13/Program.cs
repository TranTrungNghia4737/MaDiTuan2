using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAI_by_ConsoleApp
{
    class Program
    {
        //Khai báo mảng boardGrid có diện tích 8x8 làm bàn cờ
        static int boardSize = 8;
        static int[,] boardGrid = new int[boardSize, boardSize];
        // Khai báo biến dưới để ghi lại số bước đi của quân mã
        static int attemptedMoves = 0;

        // Khai báo biến X, YMove để lưu lại số nước đi có thể đi của một quân mã
        // X là đi ngang và các giá trị âm là quân mã đi sang trái, dương là sang phải
        // y là đi dọc và các giá trị âm là đi lui về sau, dương là đi lên trước
        // Bởi vì Mã di chuyển theo hình chữ L nên với X, Y là {2,1} nghĩa là quân mã sẽ đi ngang 2 ô và đi lên 1 ô.
        static int[] XMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] YMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

        static void Main(string[] args)
        {
            solveKT();
            Console.ReadLine();
        }

        static void solveKT()
        {
            //Khởi tạo bàn cờ
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    //Đặt tất cả giá trị trong mảng là -1, các ô có giá trị là -1 nghĩa là chưa có quân mã đi tới.
                    boardGrid[i, j] = -1;
                }
            }

            // Chọn vị trí bắt đầu
            Console.WriteLine("Moi chon toa do a cua quan ma: ");
            int startX = int.Parse(Console.ReadLine());
            Console.WriteLine("Moi chon toa do b cua quan ma: ");
            int startY = int.Parse(Console.ReadLine());
            boardGrid[startX, startY] = 0;
            attemptedMoves = 0;

            //Thử nghiệm các bước đi khả thi, quay lại nếu mà bước đi đó gặp phải đường cụt
            if (!solveKTUtil(startX, startY, 1))
            {
                Console.WriteLine("Không tìm thay đuong đi khả thi cho ô {0}{1}" + startX, startY);
            }
            else
            {
                printBoard(boardGrid);
                Console.WriteLine("So đuong đi đa thu {0}", attemptedMoves);
            }

            bool solveKTUtil(int x, int y, int movecount)
            {
                // Cho người dùng thấy được đã có bao nhiêu nước đi được thí nghiệm mỗi triệu đơn vị nước đi
                attemptedMoves++;
                if (attemptedMoves % 1000000 == 0)
                    Console.WriteLine("Da thu duoc {0} đuong đi", attemptedMoves);
                int k;
                int next_X;
                int next_Y;
                if (movecount == boardSize * boardSize)
                    return true;
                // Để quân mã bắt đầu đi tuần(vì trong một lúc quân mã có tối đa 8 cách đi nên 8 sẽ là giới hạn cho vòng lặp)
                for (k = 0; k < 8; k++)
                {
                    next_X = x + XMove[k];
                    next_Y = y + YMove[k];
                    if (SafeSquare(next_X, next_Y))
                    {

                        boardGrid[next_X, next_Y] = movecount;
                        if (solveKTUtil(next_X, next_Y, movecount + 1))
                            return true;
                        else
                            boardGrid[next_X, next_Y] = -1;
                    }

                }
                return false;
            }
            bool SafeSquare(int x, int y)
            {
                // Kiểm tra xem quân mã có đang chuẩn bi chạy khỏi bàn cờ hay không
                // Kiểm tra xem các ô cờ đã được đi tới chưa
                if (x >= 0 && x < boardSize && y >= 0 && y < boardSize && boardGrid[x, y] == -1)
                    return true;
                else
                    return false;

            }

            void printBoard(int[,] board)
            {

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // In ra ký tự của hình vuông
                        Console.Write(boardGrid[i, j] + " ");
                    }
                    // In xuống dòng
                    Console.WriteLine();
                }
            }
        }
    }
}