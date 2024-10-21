using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace CalculatorSolution
{
    enum OperationMode { MathOperation, MemoryOperations, HistoryOperations }
    enum AddOrRemove { Add, Remove }
    public partial class MainCalculator : Form
    {
        private bool decimalUsed = false;
        private bool completedOperation = false;
        private int currentHistory = 0;

        private OperationMode currentMode = OperationMode.MathOperation;

        private Button[] numberPadPositionButtonArray;
        private Button[] nonClearButtonArray;
        private Button[] AllButtonsArray;
        private Button[] OperationButtonArray;

        private EventHandler[] MathOpsDefaultNumPadEvents;
        private EventHandler[] MathDefaultOperationBtnEvents;

        private EventHandler[] MemoryDefaultNumPadEvents;
        private EventHandler[] MemoryDefaultOpertionBtnEvents;

        private EventHandler[] HistoryTopRowControlEvents;
        private Button[] HistoryTopRowBtnArray;


        private Button[] DisabledHistoySecondaryBtnArray;
        private Button[] DisableHistoryButtonArray;

        private FlowLayoutPanel mFlowLayoutPanel;

        public MainCalculator()
        {
            InitializeComponent();
        }

        private void MainCalculator_Load(object sender, EventArgs e)
        {
            Utility.MainDisplay = lblCurrentScreen;

            DefineAllButtonArrays();
            DefineAllEventHandlerArrays();
        }
        private void DefineAllButtonArrays()
        {
            numberPadPositionButtonArray = new Button[] { btnPosition1, btnPosition2, btnPosition3, btnPosition4, btnPosition5, btnPosition6, btnPosition7, btnPosition8, btnPosition9 };

            nonClearButtonArray = new Button[] { btnModeControl, btnClearEntry, btnPositionClear, btnPositionDivide, btnPositionPlus, btnPositionMemory, btnPosition7, btnPosition8, btnPosition9, btnPositionPlus, btnUpHis, btnPosition4, btnPosition5, btnPosition6, btnBackOneDigit, btnPosition1, btnPosition2, btnPosition3, btnDownHis, btnPositionPlusMinus, btnPosition0, btnPositonPeriod };

            AllButtonsArray = new Button[] { btnModeControl, btnClearEntry, btnPositionClear, btnPositionDivide, btnPositionPlus, btnPositionMemory, btnPosition7, btnPosition8, btnPosition9, btnPositionPlus, btnUpHis, btnPosition4, btnPosition5, btnPosition6, btnBackOneDigit, btnPosition1, btnPosition2, btnPosition3, btnDownHis, btnPositionPlusMinus, btnPosition0, btnPositonPeriod, btnPositionEnter, btnPositionMinus };

            OperationButtonArray = new Button[] { btnPositionMultiply, btnPositionDivide, btnPositionPlus, btnPositionMinus };




            DisabledHistoySecondaryBtnArray = new Button[] {
                btnPositionMemory, btnUpHis, btnDownHis, btnBackOneDigit, btnPositionPlusMinus,
                btnPositonPeriod, btnPositionPlus, btnPositionMinus, btnPositionEnter
            };
            DisableHistoryButtonArray = numberPadPositionButtonArray.Union(DisabledHistoySecondaryBtnArray).ToArray();

            HistoryTopRowBtnArray = new Button[]
                { btnClearEntry, btnPositionClear, btnPositionMultiply, btnPositionDivide };

        }

        private void DefineAllEventHandlerArrays()
        {
            MathOpsDefaultNumPadEvents = new EventHandler[] { btnPosition1_Click, btnPosition2_Click, btnPosition3_Click, btnPosition4_Click, btnPosition5_Click, btnPosition6_Click, btnPosition7_Click, btnPosition8_Click, btnPosition9_Click };

            MemoryDefaultNumPadEvents = new EventHandler[] { btnPosition1_Memory_Click, btnPosition2_Memory_Click, btnPosition3_Memory_Click, btnPosition4_Memory_Click, btnPosition5_Memory_Click, btnPosition6_Memory_Click, btnPosition7_Memory_Click, btnPosition8_Memory_Click, btnPosition9_Memory_Click };

            MemoryDefaultOpertionBtnEvents = new EventHandler[] { btnPositionMultiply_Memory_Click, btnPositionDivide_Memory_Click, btnPositionPlus_Memory_Click, btnPositionMinus_Memory_Click };

            MathDefaultOperationBtnEvents = new EventHandler[] { btnPositionMultiply_Click, btnPositionDivide_Click, btnPositionMinus_Click, btnPositionPlus_Click };

            HistoryTopRowControlEvents = new EventHandler[]
            {
                btnPositionClearEntry_History_Click, btnPositionClear_History_Click,
                btnPositionMultiply_History_Click, btnPositionDivide_History_Click
            };

        }



        #region"Math Operations"
        //number pad events
        private void btnPosition1_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition1);
        }
        private void btnPosition2_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition2);
        }
        private void btnPosition3_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition3);
        }
        private void btnPosition4_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition4);
        }
        private void btnPosition5_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition5);
        }
        private void btnPosition6_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition6);
        }
        private void btnPosition7_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition7);
        }
        private void btnPosition8_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition8);
        }
        private void btnPosition9_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition9);
        }
        private void btnPosition0_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            AddNumberToString(btnPosition0);
        }
        private void btnPositionPeriod_Click(object sender, EventArgs e)
        {
            CheckForCompleted();
            if (Utility.NumberStringBuilder != null && !decimalUsed)
            {
                Utility.NumberStringBuilder.Append(".");
                decimalUsed = true;
            }
            Utility.UpdateMainDisplay();
        }
        private void btnPositionPlusMinus_Click(object sender, EventArgs e)
        {
            if (Utility.NumberStringBuilder.ToString().StartsWith("-"))
            {
                Utility.NumberStringBuilder.Remove(0, 1);
            }
            else
            {
                Utility.NumberStringBuilder.Insert(0, "-1");
            }
            Utility.UpdateMainDisplay();
        }

        //operation btn events
        private void btnPositionPlus_Click(object sender, EventArgs e)
        {
            OperationToString("+");
        }
        private void btnPositionMultiply_Click(object sender, EventArgs e)
        {
            OperationToString("*");
        }
        private void btnPositionDivide_Click(object sender, EventArgs e)
        {
            OperationToString("/");
        }
        private void btnPositionMinus_Click(object sender, EventArgs e)
        {
            OperationToString("-");
        }



        //secondary button events
        private void btnPositionEnter_Click(object sender, EventArgs e)
        {
            Utility.FullSetOfNumbersAndOperators.Add(Utility.NumberStringBuilder.ToString());

            OperationChunk operationChunk = new OperationChunk();
            operationChunk.EquationStrings = Utility.FullSetOfNumbersAndOperators;

            MathOperations mathOperations = new MathOperations(operationChunk);
            Utility.CalculateMainDisplay(operationChunk);
            operationChunk.SolvedString = mathOperations.CompleteEquationString();

            completedOperation = true;

            Utility.History.Add(operationChunk);
        }


        private void btnUpHis_Click(object sender, EventArgs e)
        {
            int totalEquations = Utility.History.Count;
            lblSecondaryMain.Text = $"{(currentHistory + 1).ToString()}/{totalEquations}";

            if (Utility.History.Count > 2 && currentHistory > 0 && currentHistory < Utility.History.Count - 1)
            {
                lblSecondaryTop.Text = Utility.History[totalEquations - currentHistory - 2].SolvedString;
                lblCurrentScreen.Text = Utility.History[totalEquations - 1 - currentHistory].SolvedString;
                lblSecondaryBttm.Text = Utility.History[totalEquations - currentHistory].SolvedString;
                currentHistory++;
            }
            else if (Utility.History.Count > 2 && currentHistory < Utility.History.Count - 1)
            {
                lblSecondaryTop.Text = Utility.History[totalEquations - currentHistory - 2].SolvedString;
                lblCurrentScreen.Text = Utility.History[totalEquations - 1 - currentHistory].SolvedString;
                lblSecondaryBttm.Text = "↑↑";
                currentHistory++;
            }
            else if (Utility.History.Count > 2 && currentHistory == Utility.History.Count - 1)
            {
                lblCurrentScreen.Text = Utility.History[totalEquations - 1 - currentHistory].SolvedString;
                lblSecondaryBttm.Text = Utility.History[totalEquations - currentHistory].SolvedString;
                lblSecondaryTop.Text = "No further Equations";
            }
        }
        private void btnDownHis_Click(object sender, EventArgs e)
        {
            int totalEquations = Utility.History.Count;
            lblSecondaryMain.Text = $"{(currentHistory + 1)}/{totalEquations}";
            if (Utility.History.Count > 2 && currentHistory == Utility.History.Count - 1)
            {
                lblSecondaryTop.Text = "↓↓";
                lblCurrentScreen.Text = Utility.History[totalEquations - currentHistory - 1].SolvedString;
                lblSecondaryBttm.Text = Utility.History[totalEquations - currentHistory].SolvedString;
                currentHistory--;
            }
            else if (Utility.History.Count > 2 && currentHistory > 1)
            {
                lblSecondaryTop.Text = Utility.History[totalEquations - currentHistory].SolvedString;
                lblCurrentScreen.Text = Utility.History[totalEquations - currentHistory + 1].SolvedString;
                lblSecondaryBttm.Text = Utility.History[totalEquations - currentHistory + 2].SolvedString;
                currentHistory--;
            }
            else if (Utility.History.Count > 2 && currentHistory == 1)
            {
                lblSecondaryTop.Text = Utility.History[totalEquations - currentHistory].SolvedString;
                lblCurrentScreen.Text = Utility.History[totalEquations - currentHistory - 1].SolvedString;
                lblSecondaryBttm.Text = "↑↑";
            }
        }
        private void btnBackOneDigit_Click(object sender, EventArgs e)
        {
            currentHistory = 0;

        }




        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            if (Utility.NumberStringBuilder != null && !completedOperation)
            {
                Utility.NumberStringBuilder = null;
                Utility.NumberStringBuilder = new StringBuilder();
                Utility.UpdateMainDisplay();
            }
        }
        private void btnPositionClear_Click(object sender, EventArgs e)
        {
            if (completedOperation)
            {
                Utility.NumberStringBuilder = null;
                Utility.FullSetOfNumbersAndOperators = new List<string>();
                Utility.History.RemoveAt(Utility.History.Count - 1);
                lblCurrentScreen.Text = "Equation Removed";
                completedOperation = false;
            }
            else if (lblCurrentScreen.Text != String.Empty)
            {
                Utility.NumberStringBuilder = null;
                Utility.FullSetOfNumbersAndOperators = new List<string>();
                Utility.operation = null;
                completedOperation = false;
            }

            currentHistory = 0;
            lblSecondaryTop.Text = "↑";
            lblSecondaryBttm.Text = "↓";
        }

        //math helper functions
        private void CheckForCompleted()
        {
            if (completedOperation)
            {
                Utility.NumberStringBuilder = new System.Text.StringBuilder();
                Utility.FullSetOfNumbersAndOperators = new List<string>();
                Utility.operation = null;
                Utility.MainDisplay.Text = String.Empty;
                completedOperation = false;
            }
        }
        private void OperationToString(string op)
        {
            if (Utility.operation == null)
            {
                Utility.FullSetOfNumbersAndOperators.Add(Utility.NumberStringBuilder.ToString());

                Utility.NumberStringBuilder = null;
                Utility.operation = op;
                decimalUsed = false;
            }
            else
            {
                Utility.operation = op;
            }

            Utility.UpdateMainDisplay();
        }
        private void AddNumberToString(Button position)
        {
            if (Utility.operation != null)
            {
                Utility.FullSetOfNumbersAndOperators.Add(Utility.operation);
                Utility.operation = null;
            }

            if (Utility.NumberStringBuilder == null)
                Utility.NumberStringBuilder = new System.Text.StringBuilder(position.Text.Last().ToString());
            else
                Utility.NumberStringBuilder.Append(position.Text.Last().ToString());


            Utility.UpdateMainDisplay();
        }
        #endregion



        #region QuickMemory
        private void btnPositionMemory_Click(object sender, EventArgs e)
        {
            SetMemoryFoundColor();
            SetAllQuickMemoryFromMathOperations();
        }
        private void btnPosition1_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M1", btnPosition1);
        }
        private void btnPosition2_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M2", btnPosition2);
        }
        private void btnPosition3_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M3", btnPosition3);
        }
        private void btnPosition4_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M4", btnPosition4);
        }
        private void btnPosition5_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M5", btnPosition5);
        }
        private void btnPosition6_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M6", btnPosition6);
        }
        private void btnPosition7_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M7", btnPosition7);
        }
        private void btnPosition8_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M8", btnPosition8);
        }
        private void btnPosition9_QuickMemory_Click(object sender, EventArgs e)
        {
            QuickMemoryAddOrReject("M9", btnPosition9);
        }
        private void QuickMemoryAddOrReject(string btnLookUp, Button btn)
        {
            if (btn.BackColor != System.Drawing.SystemColors.Control)
            {
                lblCurrentScreen.Text = "No value saved.";
            }
            else
            {
                AddMemoryToString(btnLookUp);
            }
            SetAllQuickMemoryToMathOperations();
            SetDefaultMathOperationsKeyColor();
        }
        private void AddMemoryToString(string btnStr)
        {
            if (Utility.operation != null)
            {
                Utility.FullSetOfNumbersAndOperators.Add(Utility.operation);
                Utility.operation = null;
            }

            Utility.NumberStringBuilder = new System.Text.StringBuilder(Utility.Memory[btnStr].SolvedValue.ToString());

            Utility.UpdateMainDisplay();
        }
        private void SetAllQuickMemoryToMathOperations()
        {

            btnPosition1.Click -= btnPosition1_QuickMemory_Click;
            btnPosition2.Click -= btnPosition2_QuickMemory_Click;
            btnPosition1.Click -= btnPosition3_QuickMemory_Click;
            btnPosition2.Click -= btnPosition4_QuickMemory_Click;
            btnPosition1.Click -= btnPosition5_QuickMemory_Click;
            btnPosition2.Click -= btnPosition6_QuickMemory_Click;
            btnPosition1.Click -= btnPosition7_QuickMemory_Click;
            btnPosition2.Click -= btnPosition8_QuickMemory_Click;
            btnPosition1.Click -= btnPosition9_QuickMemory_Click;

            btnPosition1.Click += btnPosition1_Click;
            btnPosition2.Click += btnPosition2_Click;
            btnPosition3.Click += btnPosition3_Click;
            btnPosition4.Click += btnPosition4_Click;
            btnPosition5.Click += btnPosition5_Click;
            btnPosition6.Click += btnPosition6_Click;
            btnPosition7.Click += btnPosition7_Click;
            btnPosition8.Click += btnPosition8_Click;
            btnPosition9.Click += btnPosition9_Click;

        }
        private void SetAllQuickMemoryFromMathOperations()
        {
            btnPosition1.Click -= btnPosition1_Click;
            btnPosition2.Click -= btnPosition2_Click;
            btnPosition3.Click -= btnPosition3_Click;
            btnPosition4.Click -= btnPosition4_Click;
            btnPosition5.Click -= btnPosition5_Click;
            btnPosition6.Click -= btnPosition6_Click;
            btnPosition7.Click -= btnPosition7_Click;
            btnPosition8.Click -= btnPosition8_Click;
            btnPosition9.Click -= btnPosition9_Click;

            btnPosition1.Click += btnPosition1_QuickMemory_Click;
            btnPosition2.Click += btnPosition2_QuickMemory_Click;
            btnPosition3.Click += btnPosition3_QuickMemory_Click;
            btnPosition4.Click += btnPosition4_QuickMemory_Click;
            btnPosition5.Click += btnPosition5_QuickMemory_Click;
            btnPosition6.Click += btnPosition6_QuickMemory_Click;
            btnPosition7.Click += btnPosition7_QuickMemory_Click;
            btnPosition8.Click += btnPosition8_QuickMemory_Click;
            btnPosition9.Click += btnPosition9_QuickMemory_Click;
        }
        #endregion



        #region Memory
        string memoryEditCurrent;
        bool clearAll = false;
        string memorySlotTofill;

        private void btnPosition1_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition1);
            EnableClearAndStore();
        }
        private void btnPosition2_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition2);
            EnableClearAndStore();

        }
        private void btnPosition3_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition3);
            EnableClearAndStore();

        }
        private void btnPosition4_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition4);
            EnableClearAndStore();

        }
        private void btnPosition5_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition5);
            EnableClearAndStore();

        }
        private void btnPosition6_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition6);
            EnableClearAndStore();

        }
        private void btnPosition7_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition7);
            EnableClearAndStore();

        }
        private void btnPosition8_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition8);
            EnableClearAndStore();

        }
        private void btnPosition9_Memory_Click(object sender, EventArgs e)
        {
            DisplayMemoryValue(btnPosition9);
            EnableClearAndStore();

        }



        //clear all memory values
        private void btnPositionMultiply_Memory_Click(object sender, EventArgs e)
        {
            lblCurrentScreen.Text = "Press enter to confirm, return to cancel";
            clearAll = true;
            btnPositionEnter.Click -= btnPositionEnter_Memory_Click;
            btnPositionEnter.Click += btnPositionEnter_Memory_ConfirmClear_Click;
        }
        //clear single memory value
        private void btnPositionDivide_Memory_Click(object sender, EventArgs e)
        {
            if (Utility.Memory.ContainsKey(memoryEditCurrent))
            {
                lblCurrentScreen.Font = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                lblCurrentScreen.Text = $"Press enter or return \nto confirm clearing {memoryEditCurrent}";

                btnPositionEnter.Click -= btnPositionEnter_Memory_Click;
                btnPositionEnter.Click += btnPositionEnter_Memory_ConfirmClear_Click;

                btnPositionMinus.Click -= btnPositionMinus_Memory_Click;
                btnPositionMinus.Click += btnPositionMinus_Memory_Cancel_Click;

                DisableAllButReturnAndEnter();
                btnPositionMultiply.Enabled = false;
            }
        }


        //store equation or number
        private void btnPositionPlus_Memory_Click(object sender, EventArgs e)
        {
            if (!Utility.Memory.ContainsKey(memoryEditCurrent))
            {
                memorySlotTofill = lblSecondaryMain.Text;
                //all number buttons turn to numbers-->possibly prompt user for equation store or for number store
                //equations can be computed and saved-->this will set the enter function to be looking for an operand
                //single numbers can be entered and saved-->this will set the enter function delegate to not consider an operand

                btnPositionEnter.Click -= btnPositionEnter_Memory_Click;
                btnPositionEnter.Click += btnPositionEnter_Memory_ConfirmStore_Click;
            }
        }

        //return button
        private void btnPositionMinus_Memory_Click(object sender, EventArgs e)
        {
            InitializeModeFunctions();
            InitializeModeFunctions();
            lblCurrentScreen.Text = "";
            memoryEditCurrent = null;
        }
        private void btnPositionEnter_Memory_Click(Object sender, EventArgs e)
        {
            lblCurrentScreen.Text = String.Empty;
            lblSecondaryMain.Text = "Mem Edit";
        }

        private void btnPositionMemory_Memory_Click(Object sender, EventArgs e)
        {
            lblCurrentScreen.Text = "some info descriping what is going on";
        }

        private void btnPositionEnter_Memory_ConfirmClear_Click(object sender, EventArgs e)
        {
            if (clearAll)
            {
                Utility.Memory.Clear();
                lblCurrentScreen.Text = "All Memory Cleared";
                clearAll = false;
                memoryEditCurrent = null;
            }
            else
            {
                lblCurrentScreen.Text = $"{memoryEditCurrent} Cleared";
                Utility.Memory.Remove(memoryEditCurrent);
            }


            SetMemoryFoundColor();
            EnableAllMemoryButtons();
            btnPositionMultiply.Enabled = true;

            btnPositionEnter.Click -= btnPositionEnter_Memory_ConfirmClear_Click;
            btnPositionEnter.Click += btnPositionEnter_Memory_Click;

            btnPositionDivide.Enabled = false;
            btnPositionPlus.Enabled = false;
        }
        private void btnPositionMinus_Memory_Cancel_Click(object sender, EventArgs e)
        {
            lblCurrentScreen.Text = "Cancelled";
            clearAll = false;
            EnableAllMemoryButtons();
            btnPositionMultiply.Enabled = true;
        }
        private void btnPositionEnter_Memory_ConfirmStore_Click(object sender, EventArgs e)
        {
            OperationChunk chunk = new OperationChunk();
            chunk.SolvedString = "5+5=10";
            Utility.Memory.Add(memorySlotTofill, chunk);

            lblCurrentScreen.Text = "Added";
            memorySlotTofill = null;
            SetMemoryFoundColor();

            btnPositionEnter.Click -= btnPositionEnter_Memory_ConfirmStore_Click;
            btnPositionEnter.Click += btnPositionEnter_Memory_Click;
        }
        private void EnableClearAndStore()
        {
            btnPositionDivide.Enabled = true;
            btnPositionPlus.Enabled = true;
        }
        private void DisplayMemoryValue(Button btn)
        {
            memoryEditCurrent = null;
            memoryEditCurrent = btn.Text;
            if (Utility.Memory.ContainsKey(btn.Text))
            {
                lblCurrentScreen.Text = Utility.Memory[btn.Text].SolvedString;
            }
            else
                lblCurrentScreen.Text = "Empty";

            lblSecondaryMain.Text = btn.Text;
        }



        private void EnableAllMemoryButtons()
        {
            foreach (var btn in nonClearButtonArray)
            {
                btn.Enabled = true;
            }
        }
        private void DisableAllButReturnAndEnter()
        {
            foreach (var btn in nonClearButtonArray)
            {
                btn.Enabled = false;
            }
        }
        #endregion



        #region"History"
        private void btnPositionClearEntry_History_Click(object sender, EventArgs e)
        {
            //down arrow
        }
        private void btnPositionClear_History_Click(object sender, EventArgs e)
        {
            //up arrow
        }
        private void btnPositionMultiply_History_Click(object sender, EventArgs e)
        {
            //select file to save these too
        }
        private void btnPositionDivide_History_Click(object sender, EventArgs e)
        {
            //clear all history from file
        }

        private void PrintTheTag(object sender, EventArgs e)
        {
            lblCurrentScreen.Text = "worked";
            ;
        }
        
        
        #endregion






        #region Mode Control
        private void SetModeFunction(OperationMode mode)
        {
            currentMode = mode;
            InitializeModeFunctions();
        }
        private void btnModeControl_Click(object sender, EventArgs e)
        {
            CycleThroughModes();
            InitializeModeFunctions();
        }
        private void CycleThroughModes()
        {
            if (currentMode == OperationMode.HistoryOperations)
            {
                currentMode = OperationMode.MathOperation;
            }
            else
            {
                int current = (int)currentMode + 1;
                currentMode = (OperationMode)current;
            }
        }

        private void InitializeModeFunctions()
        { //1) Set text for select operation 2)Enable/disable any particular set of buttons 3)Remove previous operation events 4)Add current operation events 5)Handle any special case btn/events 6)Set any text changes necessary 7)Alter button colors if necessary
            switch (currentMode)
            {
                case OperationMode.MathOperation:

                    //1
                    lblSecondaryTop.Text = "↑";
                    lblSecondaryMain.Text = "Oper";
                    lblSecondaryBttm.Text = "↓";
                    lblCurrentScreen.Text = String.Empty;

                    //2
                    EnableAllButtons();
                    gBHistoryDisplay.Visible = false;

                    //3 
                    AddRemoveDefaultHistoryEvents_TopRow(AddOrRemove.Remove);

                    //4
                    AddRemoveDefaultMathEvents_NumPad(AddOrRemove.Add);
                    AddRemoveDefaultMathEvents_OperationBtn(AddOrRemove.Add);
                    AddRemoveDefaultMathEvents_SecondaryBtn(AddOrRemove.Add);

                    //5
                    //comeback to any special keys

                    //6
                    btnClearEntry.Text = "CE";
                    btnPositionClear.Text = "C";
                    SetDefaultMathNumberPadText();
                    SetDefaultMathOperationKeyText();
                    SetDefaultMathSecondaryText();

                    //7
                    SetDefaultMathOperationsKeyColor();
                    break;

                //1) Set text for select operation 2)Enable/disable any particular set of buttons 3)Remove previous operation events 4)Add current operation events 5)Handle any special case btn/events 6)Set any text changes necessary 7)Alter button colors if necessary
                case OperationMode.MemoryOperations:

                    //1
                    lblSecondaryMain.Text = "Mem Edit";
                    lblSecondaryTop.Text = "↑";
                    lblCurrentScreen.Text = String.Empty;
                    lblSecondaryBttm.Text = "↓";

                    //2
                    btnPositionDivide.Enabled = false;
                    btnPositionPlus.Enabled = false;
                    btnPositionMinus.Enabled = false;
                    btnPositionPlusMinus.Enabled = false;
                    btnPosition0.Enabled = false;
                    btnPositonPeriod.Enabled = false;
                    btnClearEntry.Enabled = false;
                    btnPositionClear.Enabled = false;

                    //3
                    AddRemoveDefaultMathEvents_NumPad(AddOrRemove.Remove);
                    AddRemoveDefaultMathEvents_OperationBtn(AddOrRemove.Remove);


                    //4
                    AddRemoveDefaultMemoryEvents_NumPad(AddOrRemove.Add);
                    AddRemoveDefaultMemoryEvents_OperationBtn(AddOrRemove.Add);


                    //5
                    AddRemoveDefaultMemoryEvents_SecondaryBtn(AddOrRemove.Add);
                    AddRemoveEnterButtonEvents(AddOrRemove.Remove);
                    AddRemoveEnterButtonEvents(AddOrRemove.Add);


                    //6
                    SetDefaultMemoryNumPadText();
                    SetDefaultMemoryOperationKeyText();
                    SetDefaultMemorySecondaryText();

                    //7
                    SetMemoryFoundColor();

                    break;

                //1) Set text for select operation 2)Enable/disable any particular set of buttons 3)Remove previous operation events 4)Add current operation events 5)Handle any special case btn/events 6)Set any text changes necessary 7)Alter button colors if necessary
                case OperationMode.HistoryOperations:

                    //1
                    lblSecondaryMain.Text = "His";
                    lblSecondaryTop.Text = "↑";
                    lblCurrentScreen.Text = String.Empty;
                    lblSecondaryBttm.Text = "↓";


                    //2
                    btnClearEntry.Enabled = true;
                    btnPositionClear.Enabled = true;
                    btnPositionMultiply.Enabled = true;
                    btnPositionDivide.Enabled = true;

                    foreach (Button btn in DisableHistoryButtonArray) //disable all buttons minus Top row
                    {
                        btn.Enabled = false;
                    }

                    //3
                    AddRemoveDefaultMathEvents_NumPad(AddOrRemove.Remove);
                    AddRemoveDefaultMathEvents_OperationBtn(AddOrRemove.Remove);
                    AddRemoveDefaultMemoryEvents_SecondaryBtn(AddOrRemove.Remove);

                    //4
                    AddRemoveDefaultHistoryEvents_TopRow(AddOrRemove.Add);


                    gBHistoryDisplay.Visible = true;
                    gBHistoryDisplay.Location = new Point(30, 205);
                    mFlowLayoutPanel = new FlowLayoutPanel();
                    mFlowLayoutPanel.Dock = DockStyle.Fill;
                    gBHistoryDisplay.Controls.Add((mFlowLayoutPanel));
                    mFlowLayoutPanel.SuspendLayout();

                    foreach (OperationChunk chunk in Utility.History)
                    {
                        GroupBox box = new GroupBox();

                        Label lb = new Label();
                        lb.Text = chunk.SolvedString;
                        lb.Size = new Size(200, 25);
                        Button btn33 = new Button();
                        btn33.Tag = $"{chunk.SolvedString}";
                        btn33.Text = "Add to memory";
                        btn33.Click += PrintTheTag;

                        box.Controls.Add(lb);
                        box.Controls.Add(btn33);
                        mFlowLayoutPanel.Controls.Add(lb);
                        mFlowLayoutPanel.Controls.Add(btn33);
                    }

                    mFlowLayoutPanel.ResumeLayout();



                    //6 Set new text
                    btnClearEntry.Text = "↓";
                    btnPositionClear.Text = "↑";
                    btnPositionMultiply.Text = "File";
                    btnPositionDivide.Text = "Clear All";

                    btnPositionMultiply.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    btnPositionDivide.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    btnPositionPlus.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    btnPositionMinus.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    btnPositionMemory.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    break;
            }
        }
        #endregion


        //Across all modes
        private void AddRemoveEnterButtonEvents(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Remove)
            {
                btnPositionEnter.Click -= btnPositionEnter_Click;
                btnPositionEnter.Click -= btnPositionEnter_Memory_Click;
            }
            else
            {
                switch (currentMode)
                {
                    case OperationMode.MathOperation:
                        btnPositionEnter.Click += btnPositionEnter_Click;
                        break;
                    case OperationMode.MemoryOperations:
                        btnPositionEnter.Click += btnPositionEnter_Memory_Click;
                        break;
                }
            }
        }


        //Specific to Math mode
        private void EnableAllButtons()
        {
            foreach (var btn in AllButtonsArray)
            {
                btn.Enabled = true;
            }
        }

        private void AddRemoveDefaultMathEvents_NumPad(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Add)
            {
                for (int i = 0; i < numberPadPositionButtonArray.Length; i++)
                {
                    numberPadPositionButtonArray[i].Click += MathOpsDefaultNumPadEvents[i];
                }
            }
            else
            {
                for (int i = 0; i < numberPadPositionButtonArray.Length; i++)
                {
                    numberPadPositionButtonArray[i].Click -= MathOpsDefaultNumPadEvents[i];
                }
            }
        }
        private void AddRemoveDefaultMathEvents_OperationBtn(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Add)
            {
                for (int i = 0; i < OperationButtonArray.Length; i++)
                {
                    OperationButtonArray[i].Click += MathDefaultOperationBtnEvents[i];
                }
            }
            else
            {
                for (int i = 0; i < OperationButtonArray.Length; i++)
                {
                    OperationButtonArray[i].Click -= MathDefaultOperationBtnEvents[i];
                }
            }
        }
        private void AddRemoveDefaultMathEvents_SecondaryBtn(AddOrRemove addRemove)
        {
            btnPositionMemory.Click += btnPositionMemory_Click;
            //include the up/down arrow keys
            //include the CE and C keys
            //include the plus minus and period key
        }


        private void SetDefaultMathNumberPadText()
        {
            btnPosition1.Text = "1";
            btnPosition2.Text = "2";
            btnPosition3.Text = "3";
            btnPosition4.Text = "4";
            btnPosition5.Text = "5";
            btnPosition6.Text = "6";
            btnPosition7.Text = "7";
            btnPosition8.Text = "8";
            btnPosition9.Text = "9";
        }
        private void SetDefaultMathOperationKeyText()
        {
            btnPositionMultiply.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionMultiply.Text = "x";
            btnPositionDivide.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionDivide.Text = "/";
            btnPositionPlus.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionPlus.Text = "+";
            btnPositionMinus.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionMinus.Text = "-";
            btnPositionMemory.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
        private void SetDefaultMathSecondaryText()
        {
            btnPositionMemory.Text = "M";
            //include the up/down arrow keys
            //include the CE and C keys
            //include the plus minus and period key
        }


        private void SetDefaultMathOperationsKeyColor()
        {
            foreach (var bttn in numberPadPositionButtonArray)
            {
                bttn.BackColor = SystemColors.Control;
            }
        }


        //Specific to Memory mode
        private void AddRemoveDefaultMemoryEvents_NumPad(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Add)
            {
                for (int i = 0; i < numberPadPositionButtonArray.Length; i++)
                {
                    numberPadPositionButtonArray[i].Click += MemoryDefaultNumPadEvents[i];
                }
            }
            else
            {
                for (int i = 0; i < numberPadPositionButtonArray.Length; i++)
                {
                    numberPadPositionButtonArray[i].Click -= MemoryDefaultNumPadEvents[i];
                }
            }
        }
        private void AddRemoveDefaultMemoryEvents_OperationBtn(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Add)
            {
                for (int i = 0; i < OperationButtonArray.Length; i++)
                {
                    OperationButtonArray[i].Click += MemoryDefaultOpertionBtnEvents[i];
                }
            }
            else
            {
                for (int i = 0; i < OperationButtonArray.Length; i++)
                {
                    OperationButtonArray[i].Click -= MemoryDefaultOpertionBtnEvents[i];
                }
            }
        }
        private void AddRemoveDefaultMemoryEvents_SecondaryBtn(AddOrRemove addOrRemove)
        {
            btnBackOneDigit.Click -= btnBackOneDigit_Click;
            btnBackOneDigit.Click += btnPositionEnter_Memory_Click;


            //these buttons include the Up Arrow, Down Arrow, M, and back Arrow

        }



        private void SetDefaultMemoryNumPadText()
        {
            for (int i = 0; i < numberPadPositionButtonArray.Length; i++)
            {
                numberPadPositionButtonArray[i].Text = $"M{i + 1}";
            }
        }
        private void SetDefaultMemoryOperationKeyText()
        {
            btnPositionMultiply.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionMultiply.Text = "Clear All";

            btnPositionDivide.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionDivide.Text = "Clear";

            btnPositionPlus.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionPlus.Text = "Store";

            btnPositionMinus.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionMinus.Text = "Return";
        }
        private void SetDefaultMemorySecondaryText()
        {
            btnPositionClear.Text = "-";
            btnClearEntry.Text = "-";
            btnPositionPlusMinus.Text = "-";
            btnPosition0.Text = "-";
            btnPositonPeriod.Text = "-";


            btnPositionMemory.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPositionMemory.Text = "Info";
        }


        private void SetMemoryFoundColor()
        {
            foreach (var btn in numberPadPositionButtonArray)
            {
                if (!Utility.Memory.ContainsKey(currentMode == OperationMode.MathOperation ? $"M{btn.Text}" : btn.Text))
                {
                    btn.BackColor = SystemColors.ControlDark;
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                }
            }
        }


        //Specific to History mode
        private void AddRemoveDefaultHistoryEvents_TopRow(AddOrRemove addRemove)
        {
            if (addRemove == AddOrRemove.Add)
            {
                for (int i = 0; i < HistoryTopRowBtnArray.Length; i++)
                {
                    HistoryTopRowBtnArray[i].Click += HistoryTopRowControlEvents[i];
                }
            }
            else
            {
                for (int i = 0; i < HistoryTopRowBtnArray.Length; i++)
                {
                    HistoryTopRowBtnArray[i].Click -= HistoryTopRowControlEvents[i];
                }
            }
        }


    }
}
