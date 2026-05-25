//-----------------------------------------------------------------------
// <copyright file="ChangeViewEventArgs.cs" company="Lifeprojects.de">
//     Class: ChangeViewEventArgs
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>gerhard.ahrens@lifeprojects.de</email>
// <date>25.05.2025</date>
//
// <summary>
// Die Klasse 'ChangeViewEventArgs' wird zum weitergeben von Informationen zwischen den Dialogen Verwendet,
// </summary>
//-----------------------------------------------------------------------

namespace WPFFactory.Beispiele
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("FromPage: {this.FromPage}")]

    public partial class ChangeViewEventArgs : System.EventArgs
    {
        /// <summary>
        /// UserControl von dem der Wechsel aufgerufen wird
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Menüpunkt als Enum
        /// </summary>
        public DialogView MenuButton { get; set; }

        /// <summary>
        /// Menüpunkt als Enum
        /// </summary>
        public DialogView FromPage { get; set; }

        /// <summary>
        /// ID des Entity Objektes
        /// </summary>
        public Guid EntityId { get; set; }
    }
}
