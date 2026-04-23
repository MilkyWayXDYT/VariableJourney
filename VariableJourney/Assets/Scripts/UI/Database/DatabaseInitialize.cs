using System.IO;
using UnityEngine;
using System.Data.SQLite;

public class DatabaseInitialize : MonoBehaviour
{
    private string fileName = "ProgrammingTest.db";

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath))
            DBInitialize();
    }

    private void DBInitialize()
    {
        SQLiteConnection.CreateFile(filePath);

        DatabaseCreate();
        DatabaseFilling();
    }

    private void DatabaseCreate()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"create table Questions (
                    id integer primary key autoincrement,
                    question varchar not null
                );

                create table Answers (
                    id integer primary key autoincrement,
                    answer varchar not null,
                    isRight bit not null,
                    questionId integer not null,
                    foreign key (questionId) references questions(id)
                );
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private void DatabaseFilling()
    {
        QuestionsFilling();
        AnswersFilling();
    }

    private void QuestionsFilling()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Questions (question) values ('Что такое переменная?');
                insert into Questions (question) values ('Для чего нужна типизация?');
                insert into Questions (question) values ('Какой тип данных хранит логические значения?');
                insert into Questions (question) values ('Какой тип данных предназначен для хранения целых чисел?');
                insert into Questions (question) values ('Какой тип данных хранит текст?');
                insert into Questions (question) values ('Какие значения может принимать тип bool?');
                insert into Questions (question) values ('Что означает инициализация переменной?');
                insert into Questions (question) values ('Что делает оператор if?');
                insert into Questions (question) values ('К чему может привести использование неинициализированной переменной?');
                insert into Questions (question) values ('Что такое цикл в программировании?');
                insert into Questions (question) values ('Как работает цикл while?');
                insert into Questions (question) values ('Чем for отличается от while?');
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private void AnswersFilling()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = @"
                insert into Answers (answer, isRight, questionId) values ('Именованная область памяти, которая хранит данные', 1, 1);
                insert into Answers (answer, isRight, questionId) values ('Постоянная величина', 0, 1);
                insert into Answers (answer, isRight, questionId) values ('Имя функции', 0, 1);
                insert into Answers (answer, isRight, questionId) values ('Ошибка программы', 0, 1);

                insert into Answers (answer, isRight, questionId) values ('Чтобы компьютер знал, какой именно тип данных содержится в переменной', 1, 2);
                insert into Answers (answer, isRight, questionId) values ('Для украшения кода', 0, 2);
                insert into Answers (answer, isRight, questionId) values ('Чтобы увеличить скорость работы программы', 0, 2);
                insert into Answers (answer, isRight, questionId) values ('Для переименовывания переменных', 0, 2);

                insert into Answers (answer, isRight, questionId) values ('Bool', 1, 3);
                insert into Answers (answer, isRight, questionId) values ('String', 0, 3);
                insert into Answers (answer, isRight, questionId) values ('Int', 0, 3);
                insert into Answers (answer, isRight, questionId) values ('Float', 0, 3);

                insert into Answers (answer, isRight, questionId) values ('Int', 1, 4);
                insert into Answers (answer, isRight, questionId) values ('String', 0, 4);
                insert into Answers (answer, isRight, questionId) values ('Bool', 0, 4);
                insert into Answers (answer, isRight, questionId) values ('Float', 0, 4);

                insert into Answers (answer, isRight, questionId) values ('String', 1, 5);
                insert into Answers (answer, isRight, questionId) values ('Bool', 0, 5);
                insert into Answers (answer, isRight, questionId) values ('Int', 0, 5);
                insert into Answers (answer, isRight, questionId) values ('Float', 0, 5);

                insert into Answers (answer, isRight, questionId) values ('True и false', 1, 6);
                insert into Answers (answer, isRight, questionId) values ('Да и нет', 0, 6);
                insert into Answers (answer, isRight, questionId) values ('0 и 1', 0, 6);
                insert into Answers (answer, isRight, questionId) values ('Включено и выключено', 0, 6);

                insert into Answers (answer, isRight, questionId) values ('Присвоение переменной значения', 1, 7);
                insert into Answers (answer, isRight, questionId) values ('Объявление типа переменной', 0, 7);
                insert into Answers (answer, isRight, questionId) values ('Удаление переменной', 0, 7);
                insert into Answers (answer, isRight, questionId) values ('Переименование переменной', 0, 7);

                insert into Answers (answer, isRight, questionId) values ('Проверяет условие или наличие данных в переменной', 1, 8);
                insert into Answers (answer, isRight, questionId) values ('Создает цикл', 0, 8);
                insert into Answers (answer, isRight, questionId) values ('Объявляет новую переменную', 0, 8);
                insert into Answers (answer, isRight, questionId) values ('Вызывает функцию', 0, 8);

                insert into Answers (answer, isRight, questionId) values ('К непредсказуемому поведению программы', 1, 9);
                insert into Answers (answer, isRight, questionId) values ('К ускорению работы программы', 0, 9);
                insert into Answers (answer, isRight, questionId) values ('К автоматическому исправлению ошибок', 0, 9);
                insert into Answers (answer, isRight, questionId) values ('Ничего не произойдет', 0, 9);

                insert into Answers (answer, isRight, questionId) values ('Повторение набора инструкций до выполнения условия', 1, 10);
                insert into Answers (answer, isRight, questionId) values ('Последовательность одноразовых команд', 0, 10);
                insert into Answers (answer, isRight, questionId) values ('Однократная операция над переменной', 0, 10);
                insert into Answers (answer, isRight, questionId) values ('Ошибка при вводе данных', 0, 10);

                insert into Answers (answer, isRight, questionId) values ('Повторяет блок кода, пока не выполнено условие выхода', 1, 11);
                insert into Answers (answer, isRight, questionId) values ('Выполняется фиксированное количество раз', 0, 11);
                insert into Answers (answer, isRight, questionId) values ('Выполняется только один раз', 0, 11);
                insert into Answers (answer, isRight, questionId) values ('Зацикливается навсегда', 0, 11);

                insert into Answers (answer, isRight, questionId) values ('For используется, когда заранее известно количество повторений', 1, 12);
                insert into Answers (answer, isRight, questionId) values ('For работает медленнее', 0, 12);
                insert into Answers (answer, isRight, questionId) values ('For не может содержать условия', 0, 12);
                insert into Answers (answer, isRight, questionId) values ('For работает только с цифрами', 0, 12);
            ";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
