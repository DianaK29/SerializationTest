# SerializationTest
Фамилия, имя, отчество выполнившего тест: Карапугина Диана Владимировна
Дата выполнения: 22.06.22
Примерное количество времени, затраченного на выполнение теста: 2 часа

Задача.
Реализуйте функции сериализации и десериализации двусвязного списка, заданного следующим образом:
    class ListNode
    {
 public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
    }


    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
        }

        public void Deserialize(Stream s)
        {
        }
    }
