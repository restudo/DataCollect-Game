#include <iostream>
#include <queue>
#define N 5
#define M 1000
using namespace std;
void inisialisasi(int a, int Q[])
{
  for (int i = 0; i < N; i++)
    if ((i + 1) == a)
      Q[i] = 0;
    else
      Q[i] = M;
}
void Tampil(int data[N], char *judul)
{
  printf("%s = ", judul);
  for (int i = 0; i < N; i++)
    if (data[i] >= M)
      printf("M ");
    else
      printf("%d ", data[i]);
  printf("\n");
}

void showq(queue<int> gq)
{
  queue<int> g = gq;
  while (!g.empty())
  {
    cout << '\t' << g.front();
    g.pop();
  }
  cout << '\n';
}

void Route(int from, int to)
{
  int route[5][5] = {
      M, 1, 3, M, M,
      M, M, 1, M, 5,
      3, M, M, 2, M,
      M, M, M, M, 1,
      M, M, M, M, M};
  queue<int> list;
  list.push(0);
  queue<int> fix;
  fix.push(from);
  int curr = from - 1;
  int test = 0;

  if (to != 3)
  {
    route[2][0] = M;
  }

  while (curr != (to - 1))
  {
    // apabila curr tidak sama dengan tujuan
    int check = M;
    for (int i = 0; i < 5; i++)
    { // mencari rute yang bisa
      if (route[curr][i] < check)
      {                            // dilalui
        check = i;                 // mencek mana beban kecil
        list.push(route[curr][i]); // list beban
      }
    }

    curr = check;
    fix.push((curr + 1));

    test++;
    if (test > 7)
    {
      cout << "gagal";
      break;
    }
  }
  cout << "Rute" << endl;
  showq(fix);
  cout << "Beban" << endl;
  showq(list);
}
int main()
{
  int input[N][N] = {
      M, 1, 3, M, M,
      M, M, 1, M, 5,
      3, M, M, 2, M,
      M, M, M, M, 1,
      M, M, M, M, M};

  int Beban[N], Rute[N] = {0, 0, 0, 0, 0};
  int asal, tujuan;
  printf("Masukkan node asal : ");
  scanf("%d", &asal);
  printf("Masukkan node tujuan : ");
  scanf("%d", &tujuan);
  inisialisasi(asal, Beban);
  printf("Beban dan Rute awal\n");
  Tampil(Beban, "Beban");
  Tampil(Rute, "Rute");
  cout << "setelah algoritma djiksrat" << endl;
  Route(asal, tujuan);
}