﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Services.SqlServerCE;
using IpTviewr.Common;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Record;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.Core.IpTvProvider;
using IpTviewr.Services.EpgDiscovery;
using System.Threading;
using IpTviewr.UiServices.Common.Controls;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgMiniGuide : CommonBaseUserControl
    {
        private EpgProgram[] EpgPrograms;
        private int EpgIndex;
        private int CurrentRequestId;

        private class LoadEpgProgramsData
        {
            public int RequestId;
            public string FullServiceName;
            public string FullAlternateServiceName;
            public DateTime ReferenceTime;
            public EpgProgram[] EpgPrograms;
        } // class LoadEpgProgramsData

        public enum Button
        {
            Back,
            Forward,
            Details,
            FullView,
            EpgGrid,
            Show,
            Record,
        } // enum Button

        public event EventHandler<EpgMiniBarButtonClickedEventArgs> ButtonClicked;
        public event EventHandler<EpgMiniBarNavigationButtonsChangedEventArgs> NavigationButtonsChanged;

        #region Properties

        public UiBroadcastService SelectedService
        {
            get;
            private set;
        } // SelectedService

        public EpgProgram SelectedProgram
        {
            get { return (EpgPrograms == null) ? null : EpgPrograms[EpgIndex]; }
        } // SelectedProgram

        public DateTime LocalReferenceTime
        {
            get;
            private set;
        } // ReferenceTime

        public EpgDatastore Datastore
        {
            get;
            private set;
        } // Datastore

        /// <summary>
        /// By default, the EpgMiniBar will handle all actions invoked by the buttons by itself.
        /// If set to true, it's up to the control owner to implement the actions. For this to work, the owner has to subscribe to the ButtonClicked event.
        /// </summary>
        public bool ManualActions
        {
            get;
            set;
        } // ManualActions

        public bool IsDisabled
        {
            get;
            set;
        } // IsDisabled

        [DefaultValue(true)]
        public bool AutoRefresh
        {
            get;
            set;
        } // AutoRefresh

        public bool DetailsEnabled
        {
            get;
            set;
        }  // DetailsButtonEnabled

        public bool BasicGridEnabled
        {
            get;
            set;
        } // ButtonBasicGridEnabled

        #endregion Properties

        public EpgMiniGuide()
        {
            InitializeComponent();
            AutoRefresh = true;
        } // constructor

        #region Public methods

        public void LoadEpgPrograms(UiBroadcastService service, DateTime localReferenceTime, EpgDatastore datastore, bool async = true)
        {
            SelectedService = service;
            LocalReferenceTime = localReferenceTime.TruncateToMinutes();
            Datastore = datastore;

            // clean-up UI
            ClearEpgPrograms();
            pictureChannelLogo.SetImage(service?.Logo.GetImage(Configuration.Logos.LogoSize.Size48));

            BeginLoadEpgPrograms(async);
        } // LoadEpgPrograms

        public void ClearEpgPrograms()
        {
            timerLoadingData.Enabled = false;
            SetAutoRefreshTimer(false);
            EpgPrograms = null;
            EpgIndex = -1;

            pictureChannelLogo.SetImage(null);

            labelProgramTitle.Text = IsDisabled ? Properties.Texts.EpgNoInformation : null;
            labelEllapsed.Text = null;
            labelFromTo.Visible = false;
            labelStartTime.Visible = false;
            labelEndTime.Visible = false;
            epgProgressBar.Visible = false;

            EnableBackForward(false, false);
            buttonEpgGrid.Visible = BasicGridEnabled;
            buttonFullView.Visible = false;
            buttonDetails.Enabled = false;

            var enableButtons = (SelectedService != null) && (!SelectedService.IsHidden);
            buttonDisplayChannel.Enabled = enableButtons;
            buttonRecordChannel.Enabled = enableButtons;
        } // ClearEpgPrograms

        public void RefreshEpgPrograms(DateTime localReferenceTime)
        {
            LocalReferenceTime = localReferenceTime.TruncateToMinutes();
            BeginLoadEpgPrograms(true);
        } // RefreshEpgPrograms

        public EpgProgram[] GetEpgPrograms()
        {
            if (EpgPrograms == null) return null;

            var result = new EpgProgram[EpgPrograms.Length];
            Array.Copy(EpgPrograms, result, EpgPrograms.Length);

            return result;
        } // GetEpgPrograms

        public void GoTo(int epgIndex)
        {
            GoToNearestIndex(epgIndex);
        } // GoTo

        public void GoBack()
        {
            DisplayEpgProgram(EpgIndex - 1);
        } // GoBack

        public void GoForward()
        {
            DisplayEpgProgram(EpgIndex + 1);
        } // GoFoward

        #endregion

        #region Event handlers

        private void timerLoadingData_Tick(object sender, EventArgs e)
        {
            timerLoadingData.Enabled = false;
            labelFromTo.Text = Properties.Texts.EpgDataLoading;
            labelFromTo.Visible = true;
        } // timerLoadingData_Tick

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            SafeCall(RefreshEpgPrograms, DateTime.Now);
        } // timerAutoRefresh_Tick

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Back));
            if (ManualActions) return;

            GoBack();
        } // buttonBack_Click

        private void buttonForward_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Forward));
            if (ManualActions) return;

            GoForward();
        } // buttonForward_Click

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Details));
            if (ManualActions) return;

            // TODO: auto action
        } // buttonDetails_Click

        private void buttonFullView_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.FullView));
            if (ManualActions) return;

            // TODO: auto action
        } // buttonFullView_Click

        private void buttonEpgGrid_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.EpgGrid));

            // There's no automatic action for this button
            // It must be handled by the owner
        } // buttonEpgGrid_Click

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Show));
            if (ManualActions) return;

            ExternalTvPlayer.ShowTvChannel(ParentForm, SelectedService, true);
        } // buttonDisplayChannel_Click

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Record));
            if (ManualActions) return;

            RecordHelper.RecordProgram(ParentForm as CommonBaseForm, SelectedService, SelectedProgram, LocalReferenceTime, true);
        } // buttonRecordChannel_Click

        #endregion

        #region Auxiliary methods

        private void BeginLoadEpgPrograms(bool async)
        {
            if (IsDisabled) return;
            if (CurrentRequestId == int.MaxValue) CurrentRequestId = 0;

            if (async)
            {
                timerLoadingData.Enabled = false;
                timerLoadingData.Enabled = true;
            } // if

            // TODO: do NOT assume .imagenio.es
            var fullServiceName = SelectedService.ServiceName + ".imagenio.es";
            var fullAlternateServiceName = SelectedService.ReplacementService?.ServiceName + ".imagenio.es";

            var data = new LoadEpgProgramsData()
            {
                RequestId = Interlocked.Increment(ref CurrentRequestId),
                FullServiceName = fullServiceName,
                FullAlternateServiceName = fullAlternateServiceName,
                ReferenceTime = this.LocalReferenceTime
            };

            if (async)
            {
                ThreadPool.QueueUserWorkItem((o) => LoadEpgPrograms(data), null);
            }
            else
            {
                LoadEpgPrograms(data);
            } // if-else
        } // BeginLoadEpgPrograms

        private void LoadEpgPrograms(LoadEpgProgramsData data)
        {
            var programs = Datastore.GetPrograms(data.FullServiceName, LocalReferenceTime, 1, 2);

            // try alternate service if no EPG data
            if ((programs == null) && (data.FullAlternateServiceName != null))
            {
                programs = Datastore.GetPrograms(data.FullAlternateServiceName, LocalReferenceTime, 1, 2);
            } // if

            // populate data
            if (programs != null)
            {
                var epgPrograms = new EpgProgram[4];
                epgPrograms[0] = programs.Requested?.Previous?.Program;
                epgPrograms[1] = programs.Requested?.Program;
                var next = programs.Requested?.Next;
                epgPrograms[2] = next?.Program;
                next = next?.Next;
                epgPrograms[3] = next?.Program;

                data.EpgPrograms = epgPrograms;
            } // if

            if (InvokeRequired)
            {
                BeginInvoke(new Action<LoadEpgProgramsData>(LoadEpgProgramsEnded), data);
            }
            else
            {
                LoadEpgProgramsEnded(data);
            } // if-else
        } // LoadEpgPrograms

        private void LoadEpgProgramsEnded(LoadEpgProgramsData data)
        {
            try
            {
                // ignore data if not from current request
                // as data is loading async, "old" load request may arrive if channel is quickly changed
                if (data.RequestId != CurrentRequestId) return;

                timerLoadingData.Enabled = false;
                SetAutoRefreshTimer(true);

                EpgPrograms = data.EpgPrograms;
                buttonFullView.Enabled = (EpgPrograms != null);

                DisplayEpgProgram((data.EpgPrograms != null)? 1 : 0);
            }
            catch(Exception ex)
            {
                HandleException(new ExceptionEventData(ex));
            } // try-catch
        } // DisplayEpgPrograms

        private void DisplayEpgProgram(int index)
        {
            TimeSpan ellapsed;

            if ((index < 0) || (index > 3)) return;
            EpgIndex = index;

            epgProgressBar.Visible = (index == 1);
            labelEndTime.Visible = (index == 1);
            labelStartTime.Visible = (index == 1);
            labelFromTo.Visible = (index != 1);

            var epgProgram = EpgPrograms?[EpgIndex];

            buttonDetails.Enabled = DetailsEnabled && (epgProgram != null) && (!epgProgram.IsBlank);
            labelProgramTitle.Text = (epgProgram != null) ? epgProgram.Title : Properties.Texts.EpgNoInformation;

            if (epgProgram == null)
            {
                labelFromTo.Text = null;
                labelStartTime.Text = null;
                labelEllapsed.Text = null;
                EnableBackForward(false, false);
                return;
            } // if

            switch (EpgIndex)
            {
                case 0:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(epgProgram.LocalStartTime, epgProgram.LocalEndTime, LocalReferenceTime);
                    ellapsed = (LocalReferenceTime - epgProgram.LocalEndTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramEnded, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(false, EpgPrograms[1] != null);
                    break;
                case 1:
                    labelStartTime.Text = string.Format("{0:HH:mm}", epgProgram.LocalStartTime);
                    labelEndTime.Text = string.Format("{0:t}", epgProgram.LocalEndTime);
                    ellapsed = (LocalReferenceTime - epgProgram.LocalStartTime);
                    epgProgressBar.MaximumValue = epgProgram.Duration.TotalMinutes;
                    epgProgressBar.Value = ellapsed.TotalMinutes;
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramStarted, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(EpgPrograms[0] != null, EpgPrograms[2] != null);
                    break;
                default:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(epgProgram.LocalStartTime, epgProgram.LocalEndTime, LocalReferenceTime);
                    ellapsed = (epgProgram.LocalStartTime - LocalReferenceTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramWillStart, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(SafeGetItem(EpgPrograms, EpgIndex - 1) != null, SafeGetItem(EpgPrograms, EpgIndex + 1) != null);
                    break;
            } // switch
        } // DisplayEpgProgram

        private void GoToNearestIndex(int epgIndex)
        {
            if (epgIndex < 0) epgIndex = 0;
            if (epgIndex > 3) epgIndex = 3;

            if (EpgPrograms == null)
            {
                DisplayEpgProgram(0);
                return;
            } // if

            // look forward
            for (int index=epgIndex; index<4;index++)
            {
                if (EpgPrograms[index] != null)
                {
                    DisplayEpgProgram(index);
                    return;
                } // if
            } // for

            // look backwards
            for (int index=epgIndex; index>=0;index--)
            {
                if (EpgPrograms[index] != null)
                {
                    DisplayEpgProgram(index);
                    return;
                } // if
            } // for

            DisplayEpgProgram(0);
        } // GoToNearestIndex

        private void EnableBackForward(bool back, bool forward)
        {
            buttonBack.Enabled = back;
            buttonForward.Enabled = forward;

            NavigationButtonsChanged?.Invoke(this, new EpgMiniBarNavigationButtonsChangedEventArgs(back, forward));
        } // EnableBackForward

        private void SetAutoRefreshTimer(bool enabled)
        {
            // ensure timer ticks at :01
            timerAutoRefresh.Interval = (61 - DateTime.Now.Second) * 1000;

            timerAutoRefresh.Enabled = enabled & AutoRefresh;
        } // SetAutoRefreshTimer

        private T SafeGetItem<T>(T[] array, int index)
        {
            if ((index < 0) || (index >= array.Length)) return default(T);

            return array[index];
        } // SafeGetItem

        #endregion
    } // class EpgMiniBar
} // namespace
