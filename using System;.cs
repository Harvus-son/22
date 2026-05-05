// ╔══════════════════════════════════════════════════════════════╗
// ║         УЧЕБНЫЙ ПРОЕКТ: Windows Forms приложение            ║
// ║  Тема: Создание окна с интерактивными элементами управления ║
// ╚══════════════════════════════════════════════════════════════╝
//
// Что вы изучите в этом проекте:
//   ✔ Создание главного окна (Form)
//   ✔ Добавление элементов: Label, TextBox, Button, ProgressBar,
//                           CheckBox, TrackBar, ColorDialog
//   ✔ Обработка событий (Click, Enter, Leave, Scroll и др.)
//   ✔ Использование Timer для задержки
//   ✔ Смена темы (тёмная / светлая)
//   ✔ Диалоговые окна (MessageBox, ColorDialog)
//
// Необходимые пространства имён (библиотеки):
using System;
using System.Drawing;        // для Color, Font, Size, Point
using System.Windows.Forms;  // для всех элементов WinForms

// ──────────────────────────────────────────────────────────────
// Точка входа в программу
// ──────────────────────────────────────────────────────────────
class Program
{
    [STAThread] // Обязательный атрибут для WinForms (однопоточный режим UI)
    static void Main()
    {
        // Включаем современные визуальные стили Windows (скругления, тени и т.д.)
        Application.EnableVisualStyles();

        // Устанавливаем совместимый режим рендеринга текста
        Application.SetCompatibleTextRenderingDefault(false);

        // Запускаем приложение и открываем главное окно
        Application.Run(new MainForm());
    }
}

// ──────────────────────────────────────────────────────────────
// Главная форма (главное окно приложения)
// Наследуется от класса Form — базового класса всех окон WinForms
// ──────────────────────────────────────────────────────────────
class MainForm : Form
{
    // ═══════════════════════════════════════════════════════════
    // ПОЛЯ КЛАССА — элементы управления и данные формы
    // ═══════════════════════════════════════════════════════════

    // Элементы интерфейса:
    private Label       lblTitle;    // Заголовок окна
    private TextBox     txtName;     // Поле ввода имени
    private Button      btnGreet;    // Кнопка приветствия
    private Button      btnColor;    // Кнопка выбора цвета кнопки
    private ProgressBar progressBar; // Анимация загрузки
    private CheckBox    chkTheme;    // Переключатель темы
    private TrackBar    trkFontSize; // Ползунок размера шрифта заголовка
    private Label       lblFontHint; // Подсказка к ползунку

    // Данные:
    private bool  isDarkTheme    = true;                         // Текущая тема (тёмная по умолчанию)
    private Color btnColor_value = Color.FromArgb(0, 120, 255); // Цвет кнопки

    // ═══════════════════════════════════════════════════════════
    // КОНСТРУКТОР — вызывается при создании объекта MainForm
    // Здесь настраиваем окно и все элементы управления
    // ═══════════════════════════════════════════════════════════
    public MainForm()
    {
        InitializeWindow();    // 1. Настройка самого окна
        InitializeControls();  // 2. Создание элементов управления
        ApplyTheme();          // 3. Применение начальной темы
    }

    // ──────────────────────────────────────────────────────────
    // Метод: настройка свойств главного окна
    // ──────────────────────────────────────────────────────────
    private void InitializeWindow()
    {
        this.Text            = "🎓 Учебное приложение WinForms";  // Заголовок в строке окна
        this.Size            = new Size(500, 420);                  // Ширина x Высота в пикселях
        this.MinimumSize     = new Size(500, 420);                  // Минимальный размер (нельзя уменьшить)
        this.StartPosition   = FormStartPosition.CenterScreen;      // Открывать по центру экрана
        this.FormBorderStyle = FormBorderStyle.FixedSingle;         // Фиксированный размер (нет растягивания)
        this.MaximizeBox     = false;                               // Кнопка «развернуть» отключена
    }

    // ──────────────────────────────────────────────────────────
    // Метод: создание и настройка всех элементов управления
    // ──────────────────────────────────────────────────────────
    private void InitializeControls()
    {
        // ┌─────────────────────────────────────────────────────┐
        // │ 1. ЗАГОЛОВОК (Label)                                │
        // └─────────────────────────────────────────────────────┘
        lblTitle = new Label();
        lblTitle.Text      = "Добро пожаловать!";
        lblTitle.Font      = new Font("Segoe UI", 20, FontStyle.Bold);
        lblTitle.Size      = new Size(460, 50);
        lblTitle.Location  = new Point(20, 20);
        lblTitle.TextAlign = ContentAlignment.MiddleCenter; // Выравнивание текста по центру

        // ┌─────────────────────────────────────────────────────┐
        // │ 2. ПОЛЕ ВВОДА ИМЕНИ (TextBox)                       │
        // └─────────────────────────────────────────────────────┘
        txtName = new TextBox();
        txtName.Text      = "Введите ваше имя";
        txtName.Font      = new Font("Segoe UI", 13);
        txtName.Size      = new Size(360, 35);
        txtName.Location  = new Point(70, 90);
        txtName.ForeColor = Color.Gray; // Серый цвет — подсказка (placeholder)

        // СОБЫТИЕ Enter: срабатывает, когда поле получает фокус (пользователь кликнул)
        txtName.Enter += (s, e) =>
        {
            if (txtName.Text == "Введите ваше имя")
            {
                txtName.Text      = "";          // Очищаем текст-подсказку
                txtName.ForeColor = Color.Black; // Меняем цвет на обычный
            }
        };

        // СОБЫТИЕ Leave: срабатывает, когда поле теряет фокус (кликнули в другое место)
        txtName.Leave += (s, e) =>
        {
            if (txtName.Text == "")
            {
                txtName.Text      = "Введите ваше имя"; // Возвращаем подсказку
                txtName.ForeColor = Color.Gray;
            }
        };

        // ┌─────────────────────────────────────────────────────┐
        // │ 3. КНОПКА ПРИВЕТСТВИЯ (Button)                      │
        // └─────────────────────────────────────────────────────┘
        btnGreet = new Button();
        btnGreet.Text      = "👋 Поприветствовать";
        btnGreet.Font      = new Font("Segoe UI", 12, FontStyle.Bold);
        btnGreet.Size      = new Size(220, 44);
        btnGreet.Location  = new Point(70, 145);
        btnGreet.BackColor = btnColor_value;
        btnGreet.ForeColor = Color.White;
        btnGreet.FlatStyle = FlatStyle.Flat;         // Плоский стиль (без объёма)
        btnGreet.FlatAppearance.BorderSize = 0;      // Убираем рамку кнопки
        btnGreet.Cursor    = Cursors.Hand;           // Курсор в виде руки при наведении

        // СОБЫТИЕ Click: кнопка нажата → вызываем метод-обработчик
        btnGreet.Click += BtnGreet_Click;

        // ┌─────────────────────────────────────────────────────┐
        // │ 4. КНОПКА ВЫБОРА ЦВЕТА (Button) — новая функция!    │
        // └─────────────────────────────────────────────────────┘
        btnColor = new Button();
        btnColor.Text      = "🎨 Цвет";
        btnColor.Font      = new Font("Segoe UI", 10);
        btnColor.Size      = new Size(100, 44);
        btnColor.Location  = new Point(300, 145);
        btnColor.FlatStyle = FlatStyle.Flat;
        btnColor.FlatAppearance.BorderSize = 0;
        btnColor.Cursor    = Cursors.Hand;
        btnColor.Click    += BtnColor_Click; // Откроет палитру цветов

        // ┌─────────────────────────────────────────────────────┐
        // │ 5. ПРОГРЕСС-БАР (ProgressBar) — анимация загрузки  │
        // └─────────────────────────────────────────────────────┘
        progressBar = new ProgressBar();
        progressBar.Size     = new Size(360, 18);
        progressBar.Location = new Point(70, 205);
        progressBar.Style    = ProgressBarStyle.Marquee; // Бесконечная анимация
        progressBar.MarqueeAnimationSpeed = 30;          // Скорость анимации
        progressBar.Visible  = false;                    // Скрыт по умолчанию

        // ┌─────────────────────────────────────────────────────┐
        // │ 6. ПОДСКАЗКА И ПОЛЗУНОК РАЗМЕРА ШРИФТА (TrackBar)   │
        // └─────────────────────────────────────────────────────┘
        lblFontHint = new Label();
        lblFontHint.Text     = "Размер заголовка:";
        lblFontHint.Font     = new Font("Segoe UI", 9);
        lblFontHint.Size     = new Size(200, 20);
        lblFontHint.Location = new Point(70, 238);

        trkFontSize = new TrackBar();
        trkFontSize.Minimum       = 12;  // Минимальный размер шрифта
        trkFontSize.Maximum       = 28;  // Максимальный размер шрифта
        trkFontSize.Value         = 20;  // Начальное значение
        trkFontSize.TickFrequency = 2;   // Шаг делений
        trkFontSize.Size          = new Size(360, 40);
        trkFontSize.Location      = new Point(70, 258);

        // СОБЫТИЕ Scroll: срабатывает при движении ползунка
        trkFontSize.Scroll += (s, e) =>
        {
            // Меняем размер шрифта заголовка в реальном времени
            lblTitle.Font = new Font("Segoe UI", trkFontSize.Value, FontStyle.Bold);
        };

        // ┌─────────────────────────────────────────────────────┐
        // │ 7. ПЕРЕКЛЮЧАТЕЛЬ ТЕМЫ (CheckBox) — новая функция!   │
        // └─────────────────────────────────────────────────────┘
        chkTheme = new CheckBox();
        chkTheme.Text      = "☀ Светлая тема";
        chkTheme.Font      = new Font("Segoe UI", 10);
        chkTheme.Size      = new Size(160, 28);
        chkTheme.Location  = new Point(70, 310);
        chkTheme.Checked   = false; // По умолчанию тёмная тема
        chkTheme.Cursor    = Cursors.Hand;

        // СОБЫТИЕ CheckedChanged: срабатывает при смене состояния чекбокса
        chkTheme.CheckedChanged += (s, e) =>
        {
            isDarkTheme = !chkTheme.Checked; // Тёмная = НЕ светлая
            ApplyTheme();                    // Применяем тему
        };

        // ┌─────────────────────────────────────────────────────┐
        // │ ДОБАВЛЯЕМ ВСЕ ЭЛЕМЕНТЫ НА ФОРМУ                    │
        // │ AddRange() — добавляет сразу массив элементов       │
        // │ (удобнее, чем многократные вызовы Add())            │
        // └─────────────────────────────────────────────────────┘
        this.Controls.AddRange(new Control[]
        {
            lblTitle,    // Заголовок
            txtName,     // Поле ввода
            btnGreet,    // Кнопка приветствия
            btnColor,    // Кнопка цвета
            progressBar, // Прогресс-бар
            lblFontHint, // Подсказка ползунка
            trkFontSize, // Ползунок шрифта
            chkTheme,    // Переключатель темы
        });
    }

    // ═══════════════════════════════════════════════════════════
    // МЕТОД: смена темы оформления (тёмная / светлая)
    // Принцип: меняем BackColor и ForeColor у формы и элементов
    // ═══════════════════════════════════════════════════════════
    private void ApplyTheme()
    {
        // Определяем цвета в зависимости от темы
        Color bg        = isDarkTheme ? Color.FromArgb(28, 28, 36)   : Color.FromArgb(245, 245, 250);
        Color fg        = isDarkTheme ? Color.White                   : Color.FromArgb(30, 30, 30);
        Color secondary = isDarkTheme ? Color.FromArgb(160, 160, 180) : Color.FromArgb(100, 100, 120);
        Color inputBg   = isDarkTheme ? Color.FromArgb(45, 45, 58)   : Color.White;

        // Применяем к форме
        this.BackColor = bg;

        // Применяем к каждому элементу
        lblTitle.ForeColor    = fg;
        lblFontHint.ForeColor = secondary;

        txtName.BackColor = inputBg;
        txtName.ForeColor = txtName.Text == "Введите ваше имя" ? secondary : fg;

        // Кнопку «Цвет» обновляем под тему
        btnColor.BackColor = isDarkTheme ? Color.FromArgb(60, 60, 80) : Color.FromArgb(210, 210, 230);
        btnColor.ForeColor = fg;

        chkTheme.ForeColor    = fg;
        chkTheme.BackColor    = Color.Transparent;
        trkFontSize.BackColor = bg;
    }

    // ═══════════════════════════════════════════════════════════
    // ОБРАБОТЧИК: нажатие кнопки «Поприветствовать»
    // Signature: (object sender, EventArgs e)
    //   sender — объект, который вызвал событие (кнопка)
    //   e      — данные о событии (для Click обычно пустые)
    // ═══════════════════════════════════════════════════════════
    private void BtnGreet_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim(); // Trim() убирает пробелы по краям

        // Валидация: проверяем, введено ли имя
        if (name == "" || name == "Введите ваше имя")
        {
            MessageBox.Show(
                "Пожалуйста, введите ваше имя!",
                "Поле пустое",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            txtName.Focus(); // Возвращаем фокус в поле ввода
            return;          // Прерываем выполнение метода
        }

        // Блокируем кнопку и показываем прогресс-бар
        btnGreet.Enabled    = false;
        progressBar.Visible = true;

        // ┌─────────────────────────────────────────────────────┐
        // │ ТАЙМЕР — имитация задержки (загрузки)               │
        // │ Timer.Interval — задержка в миллисекундах           │
        // │ Timer.Tick     — событие, которое сработает         │
        // └─────────────────────────────────────────────────────┘
        Timer timer = new Timer();
        timer.Interval = 1500; // 1.5 секунды

        timer.Tick += (s, ev) =>
        {
            timer.Stop();    // Останавливаем таймер
            timer.Dispose(); // Освобождаем ресурсы таймера
            progressBar.Visible = false;

            // Обновляем заголовок
            lblTitle.Text      = $"Привет, {name}!";
            lblTitle.ForeColor = Color.FromArgb(80, 220, 130); // Зелёный

            // Показываем MessageBox с приветствием
            MessageBox.Show(
                $"Привет, {name}!\nРады вас видеть!",
                "Добро пожаловать! 🎉",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Возвращаем исходный цвет заголовка после закрытия диалога
            lblTitle.ForeColor = isDarkTheme ? Color.White : Color.FromArgb(30, 30, 30);

            btnGreet.Enabled = true;
            txtName.Focus();
        };

        timer.Start(); // Запускаем таймер
    }

    // ═══════════════════════════════════════════════════════════
    // ОБРАБОТЧИК: выбор цвета кнопки через ColorDialog
    // ColorDialog — стандартный диалог Windows для выбора цвета
    // ═══════════════════════════════════════════════════════════
    private void BtnColor_Click(object sender, EventArgs e)
    {
        // Создаём диалог выбора цвета
        using (ColorDialog colorDlg = new ColorDialog())
        {
            colorDlg.Color    = btnColor_value; // Начальный цвет = текущий цвет кнопки
            colorDlg.FullOpen = true;           // Открыть расширенную палитру сразу

            // ShowDialog() показывает диалог и возвращает результат
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                btnColor_value     = colorDlg.Color; // Сохраняем выбранный цвет
                btnGreet.BackColor = btnColor_value;  // Применяем к кнопке
            }
            // Если пользователь нажал «Отмена» — ничего не меняем
        }
        // using-блок автоматически вызывает colorDlg.Dispose() при выходе
    }
}