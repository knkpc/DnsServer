﻿/*
Technitium DNS Server
Copyright (C) 2019  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System.IO;
using System.Net;
using TechnitiumLibrary.IO;

namespace DnsServerCore.Dhcp
{
    class ServerIdentifierOption : DhcpOption
    {
        #region variables

        readonly IPAddress _address;

        #endregion

        #region constructor

        public ServerIdentifierOption(Stream s)
            : base(DhcpOptionCode.ServerIdentifier)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();

            if (len != 4)
                throw new InvalidDataException();

            _address = new IPAddress(s.ReadBytes(4));
        }

        #endregion

        #region protected

        protected override void WriteOptionTo(Stream s)
        {
            s.WriteByte(4);
            s.Write(_address.GetAddressBytes());
        }

        #endregion

        #region properties

        public IPAddress Address
        { get { return _address; } }

        #endregion
    }
}
